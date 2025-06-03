using ApiPay;
using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net.Http.Headers;
using System.Text;
public class PaymentsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Mock<ILoginUseCase> _loginUseCaseMock;

    public PaymentsIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _loginUseCaseMock = new Mock<ILoginUseCase>();
    }

    [Fact]
    public async Task GivenValidPaymentRequest_WhenPostPayment_ThenReturnsCreatedStatus()
    {
        // Arrange
        var paymentRequest = new
        {
            amount = 100,
            currency = "BRL",
            description = "Pagamento de teste",
            type = "credito",
            number = "4111111111111111",
            holderName = "Fábio Marcell",
            cvv = "123",
            expirationDate = "12/25",
            installments = 1
        };

        var content = new StringContent(
            Newtonsoft.Json.JsonConvert.SerializeObject(paymentRequest),
            Encoding.UTF8,
            "application/json"
        );

        var request = new HttpRequestMessage(HttpMethod.Post, "/payments")
        {
            Content = content
        };

        // ACT
        var autenticacao = await ResolverAutenticação();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", autenticacao);
        var response = await _client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().Contain("id");
        responseBody.Should().Contain("status");
    }

    private async Task<string> ResolverAutenticação()
    {
        var request = new LoginRequest("fabio", "teste");
        var response = new LoginResponse(true, "mocked_token", "Message");
        _loginUseCaseMock.Setup(x => x.ExecuteAsync(It.IsAny<LoginRequest>())).ReturnsAsync(response);

        // Act
        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("/login", content);

        // Assert
        result.EnsureSuccessStatusCode();
        var responseString = await result.Content.ReadAsStringAsync();
        var loginResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(responseString);

        return loginResponse.Token;
    }
}