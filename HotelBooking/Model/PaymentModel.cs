using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Model
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public Guid TransactionId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal? RefundAmount { get; set; }
        public DateTime? RefundDate { get; set; }
    }
    public class PaymentRequestInitiateModel
    {
        public int BookingId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
    }
    public class PaymentResponseInitiateModel
    {
        public int PaymentId { get; set; }
        public Guid TransactionId { get; set; }
        public string? PaymentStatus { get; set; }
    }
    public class ConfirmPaymentRequest
    {
        public int PaymentID { get; set; }
        public Guid TransactionID { get; set; }
        public string? PaymentStatus { get; set; }
    }
    public class ConfirmPaymentResponse
    {
        public int PaymentID { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Message { get; set; }
    }
    public class RefundPaymentModel
    {
        public int PaymentID { get; set; }
        public decimal RefundAmount { get; set; }

    }
    public class RefundPaymentResponseModel
    {
        public int PaymentID { get; set; }
        public string? PaymentStatus { get; set; }
    }

}
