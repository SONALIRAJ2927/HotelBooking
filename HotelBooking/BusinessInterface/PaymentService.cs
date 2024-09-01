using HotelBooking.DataInterface;
using HotelBooking.Implementation;
using HotelBooking.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.BusinessInterface
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PaymentModel>> GetAllPayment()
        {
            return await _repository.GetAllPayment();
        }

        public async Task<PaymentModel> GetPaymentById(int PaymentId)
        {
            return await _repository.GetPaymentById(PaymentId);
        }
        public async Task<PaymentResponseInitiateModel> InitiatePayment(PaymentRequestInitiateModel paymentRequest)
        {
            return await _repository.InitiatePayment(paymentRequest.BookingId, paymentRequest.TotalAmount, paymentRequest.PaymentMethod);
        }
        public async Task<ConfirmPaymentResponse> ConfirmPayment(ConfirmPaymentRequest confirmPaymentRequest)
        {
            return await _repository.ConfirmPayment(confirmPaymentRequest.PaymentID , confirmPaymentRequest.TransactionID , confirmPaymentRequest.PaymentStatus);
        }

        public async Task DeletePayment(int PaymentId)
        {
            await _repository.DeletePayment(PaymentId);
        }
        public async Task<RefundPaymentResponseModel> RefundPayment(RefundPaymentModel refundPayment)

        {
            return await _repository.RefundPayment(refundPayment.PaymentID, refundPayment.RefundAmount);
        }

    }
}
