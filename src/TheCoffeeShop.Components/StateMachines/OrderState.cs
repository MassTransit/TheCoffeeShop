namespace TheCoffeeShop.Components.StateMachines
{
    using System;
    using Automatonymous;


    public class OrderState :
        SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public int CurrentState { get; set; }

        public DateTime? ReceiveTimestamp { get; set; }

        public DateTime? CreateTimestamp { get; set; }

        public DateTime? UpdateTimestamp { get; set; }
    }
}