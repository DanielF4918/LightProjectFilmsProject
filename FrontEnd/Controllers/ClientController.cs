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
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null  // Preserve property names as-is
            };
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
                Console.WriteLine($"Frontend Edit - Client ID from form: {clientViewModel.IdClient}");
                Console.WriteLine($"Frontend Edit - Client Name: {clientViewModel.FirstName} {clientViewModel.LastName}");

                if (clientViewModel.IdClient <= 0)
                {
                    Console.WriteLine("ERROR: Invalid ID - it's zero or negative!");
                    TempData["Message"] = "Error: Client ID is invalid (zero or negative)";
                    TempData["IsError"] = true;
                    return RedirectToAction(nameof(Index));
                }

                var client = new
                {
                    IdClient = clientViewModel.IdClient,
                    FirstName = clientViewModel.FirstName,
                    LastName = clientViewModel.LastName,
                    Phone = clientViewModel.Phone,
                    Email = clientViewModel.Email,
                    Location = clientViewModel.Location
                };

                var json = System.Text.Json.JsonSerializer.Serialize(client);
                Console.WriteLine($"JSON being sent to backend: {json}");

                var response = await _httpClient.PutAsJsonAsync("api/Client", client);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Edit response: {response.StatusCode}, Content: {content}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Client updated successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error updating client: {response.StatusCode}. {content}";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception updating client: {ex.Message}";
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
        var content = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"Delete response: {response.StatusCode}, Content: {content}");
        
        if (response.IsSuccessStatusCode)
        {
            TempData["Message"] = "Client deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["Message"] = $"Error deleting client: {response.StatusCode}";
            TempData["IsError"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
    catch (Exception ex)
    {
        TempData["Message"] = $"Exception deleting client: {ex.Message}";
        TempData["IsError"] = true;
        return RedirectToAction(nameof(Index));
    }
}


        [HttpGet]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                Console.WriteLine($"Getting client with ID: {id}");
                var client = await _httpClient.GetFromJsonAsync<ClientViewModel>($"api/Client/{id}", _jsonOptions);
                if (client == null)
                {
                    return NotFound($"Client with ID {id} not found");
                }
                Console.WriteLine($"Retrieved client: {client.FirstName} {client.LastName} with ID {client.IdClient}");
                return Json(client);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound($"Client with ID {id} not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving client: {ex.Message}");
                return StatusCode(500, $"Error retrieving client: {ex.Message}");
            }
        }

    }
}
