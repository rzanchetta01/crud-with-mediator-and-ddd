using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace domain.Network;

public class ResponseTemplate : ObjectResult
{
    public ResponseTemplate(HttpStatusCode code, object? value = null) : base(value)
    {
        StatusCode = (int)code;
    }
}