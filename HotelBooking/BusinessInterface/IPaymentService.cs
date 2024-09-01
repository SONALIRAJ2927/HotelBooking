using HotelBooking.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.BusinessInterface
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentModel>> GetAllPayment();
        Task<PaymentModel> GetPaymentById(int PaymentId); //direct get data from model basis of id
        Task<PaymentResponseInitiateModel> InitiatePayment(PaymentRequestInitiateModel paymentRequest);

        Task<ConfirmPaymentResponse> ConfirmPayment(ConfirmPaymentRequest confirmPaymentRequest);
        Task DeletePayment(int PaymentId); //del Hotel basis of id
        Task<RefundPaymentResponseModel> RefundPayment(RefundPaymentModel refundPaymentModel);

    }
}
