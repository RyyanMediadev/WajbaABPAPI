namespace Wajba.Models.OrdersDomain;

public class Transactions
    {
        public long Id { get; set; } // Primary key
        public string Sign { get; set; } = "+"; // Default sign ('+' or '-')
        public long OrderId { get; set; } // Foreign key to Orders
        public string TransactionNo { get; set; } // Unique transaction number
        public decimal Amount { get; set; } = 0; // Transaction amount with a default value of 0
        public string PaymentMethod { get; set; } // Payment method used
        public string Type { get; set; } = "payment"; // Default type of transaction (e.g., payment)
        public DateTime CreatedAt { get; set; } // Timestamp when transaction was created
        public DateTime UpdatedAt { get; set; } // Timestamp when transaction was updated
    }

