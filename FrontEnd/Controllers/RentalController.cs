using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Domain.Domain;

namespace FrontEnd.Controllers
{
    public class RentalController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public RentalController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null // Preserve property names as-is
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Rental/GetRentalsWithEventName");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                    return View(new List<RentalViewModel>());
                }

                var rentals = await response.Content.ReadFromJsonAsync<IEnumerable<RentalViewModel>>();
                return View(rentals ?? new List<RentalViewModel>());
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception: {ex.Message}";
                TempData["IsError"] = true;
                return View(new List<RentalViewModel>());
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(RentalViewModel rentalViewModel)
        {
            try
            {
                var rental = new
                {
                    IdEvent = rentalViewModel.IdEvent,
                    EventName = rentalViewModel.EventName,
                    RentalDate = rentalViewModel.RentalDate,
                    ReturnDate = rentalViewModel.ReturnDate,
                    TotalCost = rentalViewModel.TotalCost
                };

                var response = await _httpClient.PostAsJsonAsync("api/Rental", rental);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                }
                else
                {
                    TempData["Message"] = "Rental created successfully.";
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
        public async Task<IActionResult> Edit(RentalViewModel rentalViewModel)
        {
            try
            {
                var rental = new
                {
                    IdRental = rentalViewModel.IdRental,  
                    IdEvent = rentalViewModel.IdEvent,
                    EventName = rentalViewModel.EventName,
                    RentalDate = rentalViewModel.RentalDate,
                    ReturnDate = rentalViewModel.ReturnDate,
                    TotalCost = rentalViewModel.TotalCost
                };

                var response = await _httpClient.PutAsJsonAsync($"api/Rental/{rentalViewModel.IdRental}", rental); 
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Rental updated successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error updating rental: {response.StatusCode}. {content}";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception updating rental: {ex.Message}";
                TempData["IsError"] = true;
            }

            return RedirectToAction(nameof(Index)); 
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteUrl = $"api/Rental/{id}";
                var response = await _httpClient.DeleteAsync(deleteUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = $"API Error: {response.StatusCode}. Details: {errorContent}";
                    TempData["IsError"] = true;
                }
                else
                {
                    TempData["Message"] = "Rental deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception: {ex.Message}";
                TempData["IsError"] = true;
            }

            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public async Task<IActionResult> GetRentalDetails(int id)
        {
            try
            {
                var rental = await _httpClient.GetFromJsonAsync<RentalViewModel>($"api/Rental/{id}");
                if (rental == null)
                {
                    return NotFound($"Rental with ID {id} not found");
                }
                return Json(rental);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound($"Rental with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rental: {ex.Message}");
            }
        }
    }
}