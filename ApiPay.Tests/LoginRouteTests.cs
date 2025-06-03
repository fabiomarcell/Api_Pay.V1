using ApiPay;
using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Text;

public class PagamentosRouteTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Mock<ILoginUseCase> _loginUseCaseMock;

    public PagamentosRouteTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _loginUseCaseMock = new Mock<ILoginUseCase>();
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenCredentialsAreValid()
    {
        // Arrange
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
        Assert.True(loginResponse.IsSuccess);
    }

    /*[Fact]
    public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = new LoginRequest { Email = "invalid", Password = "wrong" };
        var response = new LoginResponse { IsSuccess = false };
        _loginUseCaseMock.Setup(x => x.ExecuteAsync(It.IsAny<LoginRequest>())).ReturnsAsync(response);

        // Act
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("/login", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task Login_ReturnsInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var request = new LoginRequest { Email = "fabio", Password = "teste" };
        _loginUseCaseMock.Setup(x => x.ExecuteAsync(It.IsAny<LoginRequest>())).ThrowsAsync(new System.Exception("Internal error"));

        // Act
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("/login", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.InternalServerError, result.StatusCode);
    }*/
}