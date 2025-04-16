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
    public class EquipmentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EquipmentController(IHttpClientFactory httpClientFactory)
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
                var response = await _httpClient.GetAsync("api/Equipment");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                    return View(new List<EquipmentViewModel>());
                }

                var equipment = await response.Content.ReadFromJsonAsync<IEnumerable<EquipmentViewModel>>(_jsonOptions);
                return View(equipment ?? new List<EquipmentViewModel>());
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception: {ex.Message}";
                TempData["IsError"] = true;
                return View(new List<EquipmentViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EquipmentViewModel equipmentViewModel)
        {
            try
            {
                var equipment = new
                {
                    EquipmentName = equipmentViewModel.EquipmentName,
                    Description = equipmentViewModel.Description,
                    Category = equipmentViewModel.Category,
                    DailyRate = equipmentViewModel.DailyRate
                };

                var response = await _httpClient.PostAsJsonAsync("api/Equipment", equipment);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                }
                else
                {
                    TempData["Message"] = "Equipment added successfully.";
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EquipmentViewModel equipmentViewModel)
        {
            try
            {
                var equipment = new
                {
                    IdEquipment = equipmentViewModel.IdEquipment, 
                    EquipmentName = equipmentViewModel.EquipmentName,
                    Description = equipmentViewModel.Description,
                    Category = equipmentViewModel.Category,
                    DailyRate = equipmentViewModel.DailyRate
                };

                var response = await _httpClient.PutAsJsonAsync($"api/Equipment/{equipmentViewModel.IdEquipment}", equipment); 
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Equipment updated successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error updating equipment: {response.StatusCode}. {content}";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception updating equipment: {ex.Message}";
                TempData["IsError"] = true;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Equipment/{id}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Equipment deleted successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error deleting equipment: {response.StatusCode}";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception deleting equipment: {ex.Message}";
                TempData["IsError"] = true;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipmentById(int id)
        {
            try
            {
                var equipment = await _httpClient.GetFromJsonAsync<EquipmentViewModel>($"api/Equipment/{id}", _jsonOptions);
                if (equipment == null)
                {
                    return NotFound($"Equipment with ID {id} not found");
                }
                return Json(equipment);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound($"Equipment with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving equipment: {ex.Message}");
            }
        }
    }
}