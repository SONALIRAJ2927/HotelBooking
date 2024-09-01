using HotelBooking.DataInterface;
using HotelBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.PortableExecutable;

namespace HotelBooking.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<PaymentModel> _entities;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<PaymentModel>();
        }

        public async Task<IEnumerable<PaymentModel>> GetAllPayment()
        {
            return await _entities.ToListAsync();
        }

        public async Task<PaymentModel> GetPaymentById(int PaymentId)
        {
            return await _entities.FindAsync(PaymentId);
        }
       
        public async Task<PaymentResponseInitiateModel> InitiatePayment(int BookingId, decimal TotalAmount, string PaymentMethod)
        {
           
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "InitiatePayment";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters to the command
                        var transactionIdParam = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var paymentStatusParam = new SqlParameter("@PaymentStatus", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(new SqlParameter("@BookingId", BookingId));
                        command.Parameters.Add(new SqlParameter("@TotalAmount", TotalAmount));
                        command.Parameters.Add(new SqlParameter("@PaymentMethod", PaymentMethod));
                        command.Parameters.Add(transactionIdParam);
                        command.Parameters.Add(paymentStatusParam);

                        // Execute the command
                        await command.ExecuteNonQueryAsync();

                        var paymentResponse= new PaymentResponseInitiateModel
                        {
                            TransactionId = (Guid)transactionIdParam.Value,
                            PaymentStatus = paymentStatusParam.Value?.ToString()
                        };

                        return paymentResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<ConfirmPaymentResponse> ConfirmPayment(int PaymentID, Guid TransactionID, string? PaymentStatus)
        {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "ConfirmPayment";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters to the command
                        command.Parameters.Add(new SqlParameter("@PaymentID", PaymentID));
                        command.Parameters.Add(new SqlParameter("@TransactionID", TransactionID));
                        command.Parameters.Add(new SqlParameter("@PaymentStatus", PaymentStatus));

                        // Execute the command
                        await command.ExecuteNonQueryAsync();

                        // Prepare and return the response
                        var confirmPaymentResponse = new ConfirmPaymentResponse
                        {
                            PaymentID = PaymentID,
                            PaymentStatus = PaymentStatus,
                            Message = "Payment status updated successfully."
                        };

                        return confirmPaymentResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task DeletePayment(int PaymentId)
            {
                try
                {
                    // Retrieve the payment entity based on the provided PaymentId
                    var payment = await _entities.FirstOrDefaultAsync(e => e.PaymentId == PaymentId);

                    // If the payment exists, remove it and save the changes
                    if (payment != null)
                    {
                        _entities.Remove(payment);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log the error as needed
                    Console.WriteLine($"Error deleting payment: {ex.Message}");
                    throw; // Optionally, rethrow the exception to be handled by the caller
                }
            }
      
            public async Task<RefundPaymentResponseModel> RefundPayment(int paymentId, decimal refundAmount)
            {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "RefundPayment";
                        command.CommandType = CommandType.StoredProcedure;
                        // Adding parameters to the command
                        command.Parameters.Add(new SqlParameter("@PaymentID", paymentId));
                        command.Parameters.Add(new SqlParameter("@RefundAmount", refundAmount));

                      
                        // Execute the command and read the result
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var refundResponse = new RefundPaymentResponseModel
                            {
                                PaymentID = paymentId
                            };

                            if (await reader.ReadAsync())
                            {
                                refundResponse.PaymentStatus = reader.GetString(reader.GetOrdinal("Message"));
                            }

                            return refundResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            }
        }
    }


