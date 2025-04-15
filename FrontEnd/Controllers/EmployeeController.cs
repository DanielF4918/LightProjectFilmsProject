using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EmployeeController(IHttpClientFactory httpClientFactory)
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
                var response = await _httpClient.GetAsync("api/Employee");
                if (!response.IsSuccessStatusCode)
                {
                    TempData["Message"] = $"API Error: {response.StatusCode}";
                    TempData["IsError"] = true;
                    return View(new List<EmployeeViewModel>());
                }

                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>(_jsonOptions);
                return View(employees ?? new List<EmployeeViewModel>());
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Exception: {ex.Message}";
                TempData["IsError"] = true;
                return View(new List<EmployeeViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Employee", employee);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Employee added successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error: {response.StatusCode}";
                    TempData["IsError"] = true;
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
        public async Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/Employee", employee);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Employee updated successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error: {response.StatusCode}";
                    TempData["IsError"] = true;
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Employee/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Employee deleted successfully.";
                }
                else
                {
                    TempData["Message"] = $"Error: {response.StatusCode}";
                    TempData["IsError"] = true;
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
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _httpClient.GetFromJsonAsync<EmployeeViewModel>($"api/Employee/{id}", _jsonOptions);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found");
                }
                return Json(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
