﻿using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/login/login", model);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Credenciales inválidas";
                    TempData["IsError"] = true;
                    return View();
                }

                var result = await response.Content.ReadFromJsonAsync<LoginResponse>(_jsonOptions);

                if (result == null || result.Id == 0 || string.IsNullOrEmpty(result.Nombre) || string.IsNullOrEmpty(result.Rol))
                {
                    TempData["Message"] = "Error al procesar los datos de autenticación.";
                    TempData["IsError"] = true;
                    return View();
                }

                HttpContext.Session.SetString("UserId", result.Id.ToString());
                HttpContext.Session.SetString("UserName", result.Nombre);
                HttpContext.Session.SetString("UserRole", result.Rol);

                return RedirectToAction("Index", "Client");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Error inesperado: {ex.Message}";
                TempData["IsError"] = true;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }

    // ✅ Modelo para deserializar correctamente la respuesta del backend
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}