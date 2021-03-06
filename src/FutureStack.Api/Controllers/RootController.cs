﻿using FutureStack.Core.Adaptors;
using Microsoft.AspNetCore.Mvc;

namespace FutureStack.Api.Controllers
{
    [Route("/")]
    [Route("api")]
    public class RootController : Controller
    {
        private readonly IConfig _config;

        public RootController(IConfig config)
        {
            _config = config;
        }

        [HttpGet]
        public string Index()
        {
            return $"Hello yiannis, here's the key1 config: {_config.Key1}";
        }

    }
}
