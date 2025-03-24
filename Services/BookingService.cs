using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/bookings";

        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Booking>> GetBookingsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Booking>>(_baseUrl) ?? new List<Booking>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting bookings: {ex.Message}");
                return new List<Booking>();
            }
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Booking>($"{_baseUrl}/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting booking by id: {ex.Message}");
                return null;
            }
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, booking);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Booking>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating booking: {ex.Message}");
                return null;
            }
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{booking.Id}", booking);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Booking>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating booking: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteBookingAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting booking: {ex.Message}");
            }
        }

        public async Task<List<Booking>> GetUserBookingsAsync(string userId)
        {
            try
            {
                // Actual API call (commented out for now)
                // return await _httpClient.GetFromJsonAsync<List<Booking>>($"{_baseUrl}/user/{userId}") ?? new List<Booking>();

                // Mock data
                var booking = new Booking
                {
                    Id = 1,
                    EventId = 1,
                    UserId = userId.ToString(),  // Ensure conversion to string
                    Quantity = 2,
                    TotalPrice = 0,
                    CreatedAt = DateTime.Now,
                    Status = BookingStatus.Confirmed
                };

                return new List<Booking> { booking };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user bookings: {ex.Message}");
                return new List<Booking>();
            }
        }

        public async Task<bool> CancelBookingAsync(int id)
        {
            try
            {
                // Actual API call (commented out for now)
                // var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                // return response.IsSuccessStatusCode;

                // Mock data
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling booking: {ex.Message}");
                return false;
            }
        }
    }
} 