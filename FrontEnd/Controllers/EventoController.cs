using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class EventoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EventoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null
            };
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Evento");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                    return View(new List<EventoViewModel>());
                }

                var eventos = await response.Content.ReadFromJsonAsync<IEnumerable<EventoViewModel>>(_jsonOptions);
                return View(eventos ?? new List<EventoViewModel>());
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception: {ex.Message}";
                TempData["IsError"] = true;
                return View(new List<EventoViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventoViewModel eventoViewModel)
        {
            try
            {
                var evento = new
                {
                    IdEvent = 0,
                    EventName = eventoViewModel.EventName,
                    StartDate = eventoViewModel.StartDate,
                    EndDate = eventoViewModel.EndDate,
                    Location = eventoViewModel.Location,
                    IdClient = eventoViewModel.IdClient // Only send the foreign key
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(evento, _jsonOptions),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("api/Evento", jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                }
                else
                {
                    TempData["Message"] = "Event added successfully.";
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
        public async Task<IActionResult> Edit(EventoViewModel eventoViewModel)
        {
            try
            {
                if (eventoViewModel.IdEvent <= 0)
                {
                    TempData["Message"] = "Error: Event ID is invalid (zero or negative)";
                    TempData["IsError"] = true;
                    return RedirectToAction(nameof(Index));
                }

                var evento = new
                {
                    IdEvent = eventoViewModel.IdEvent,
                    EventName = eventoViewModel.EventName,
                    StartDate = eventoViewModel.StartDate,
                    EndDate = eventoViewModel.EndDate,
                    Location = eventoViewModel.Location,
                    IdClient = eventoViewModel.IdClient // Only send the foreign key
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(evento, _jsonOptions),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync("api/Evento", jsonContent);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Event updated successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error updating event: {response.StatusCode}. {content}";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception updating event: {ex.Message}";
                TempData["IsError"] = true;
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Evento/{id}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Event deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = $"Error deleting event: {response.StatusCode}";
                    TempData["IsError"] = true;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception deleting event: {ex.Message}";
                TempData["IsError"] = true;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEventoById(int id)
        {
            try
            {
                var evento = await _httpClient.GetFromJsonAsync<EventoViewModel>($"api/Evento/{id}", _jsonOptions);
                if (evento == null)
                {
                    return NotFound($"Event with ID {id} not found");
                }
                return Json(evento);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound($"Event with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving event: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Client");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, $"API Error: {response.StatusCode}");
                }

                var clients = await response.Content.ReadFromJsonAsync<IEnumerable<ClientViewModel>>(_jsonOptions);
                return Json(clients ?? new List<ClientViewModel>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving clients: {ex.Message}");
            }
        }


    }
}
