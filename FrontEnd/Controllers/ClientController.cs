using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class ClientController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Client");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                    return View(new List<ClientViewModel>());
                }

                var clients = await response.Content.ReadFromJsonAsync<IEnumerable<ClientViewModel>>(_jsonOptions);
                return View(clients ?? new List<ClientViewModel>());
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception: {ex.Message}";
                TempData["IsError"] = true;
                return View(new List<ClientViewModel>());
            }
        }


        [HttpPost]
public async Task<IActionResult> Create(ClientViewModel clientViewModel)
{
    try
    {
        var client = new
        {
            IdClient = 0,
            FirstName = clientViewModel.FirstName,
            LastName = clientViewModel.LastName,
            Phone = clientViewModel.Phone,
            Email = clientViewModel.Email,
            Location = clientViewModel.Location
        };

        var response = await _httpClient.PostAsJsonAsync("api/Client", client);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
            TempData["IsError"] = true;
        }
        else
        {
            TempData["Message"] = "Client added successfully.";
        }
    }
    catch (Exception ex)
    {
        TempData["Message"] = $"Exception: {ex.Message}";
        TempData["IsError"] = true;
    }
    return RedirectToAction(nameof(Index));
}


        [HttpPost]
        public async Task<IActionResult> Edit(ClientViewModel clientViewModel)
        {
            try
            {
                // Convert ClientViewModel to match what the backend expects
                var client = new
                {
                    IdClient = clientViewModel.IdClient,
                    FirstName = clientViewModel.FirstName,
                    LastName = clientViewModel.LastName,
                    Phone = clientViewModel.Phone,
                    Email = clientViewModel.Email,
                    Location = clientViewModel.Location
                };

                var response = await _httpClient.PutAsJsonAsync("api/Client", client);
                response.EnsureSuccessStatusCode();
                TempData["Message"] = "Client updated successfully.";
            }
            catch (HttpRequestException ex)
            {
                TempData["Message"] = $"Error updating client: {ex.Message}";
                TempData["IsError"] = true;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Client/{id}");
                response.EnsureSuccessStatusCode();
                TempData["Message"] = "Client deleted successfully.";
            }
            catch (HttpRequestException ex)
            {
                TempData["Message"] = $"Error deleting client: {ex.Message}";
                TempData["IsError"] = true;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _httpClient.GetFromJsonAsync<ClientViewModel>($"api/Client/{id}", _jsonOptions);
                return Json(client);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound($"Client with ID {id} not found");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error retrieving client: {ex.Message}");
            }
        }
    }
}
