namespace Wajba.Dtos.OrderContract;


public class OrderDTO
    {
        public OrderStatus Status { get; set; }
        public OrderType Ordertype { get; set; }
        public PaymentMethod paymentMethod { get; set; }

        public int BranchId { get; set; }

        public PickUpOrderDTO? PickUpOrder { get; set; }
        public DeliveryOrderDTO? DeliveryOrder { get; set; }
        public DriveThruOrderDTO? DriveThruOrder { get; set; }
        public DineInOrderDTO? DineInOrder { get; set; }
        public PosOrderDTO? posOrder { get; set; }
        public PosDeliveryOrderDTO? posDeliveryOrder { get; set; }

    }

