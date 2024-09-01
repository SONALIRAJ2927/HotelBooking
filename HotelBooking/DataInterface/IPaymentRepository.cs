using HotelBooking.Model;

namespace HotelBooking.DataInterface
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentModel>> GetAllPayment();
        Task<PaymentModel> GetPaymentById(int RefundPayment);
        Task<PaymentResponseInitiateModel> InitiatePayment(int BookingId, decimal TotalAmount, string PaymentMethod);
        Task<ConfirmPaymentResponse> ConfirmPayment(int paymentId, Guid transactionId, string? paymentStatus);
        Task DeletePayment(int PaymentId);
        Task<RefundPaymentResponseModel> RefundPayment(int paymentId, decimal refundAmount);

    }
}
