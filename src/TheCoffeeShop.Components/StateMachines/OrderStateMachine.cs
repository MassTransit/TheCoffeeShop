namespace TheCoffeeShop.Components.StateMachines
{
    using System;
    using Automatonymous;
    using Contracts;


    public class OrderStateMachine :
        MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState, Received, Accepted);

            Event(() => OrderReceived, x => x.CorrelateById(c => c.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(c => c.Message.OrderId));

            Initially(
                When(OrderReceived)
                    .Then(context => Touch(context.Instance, context.Data.Timestamp))
                    .Then(context => SetReceiveTimestamp(context.Instance, context.Data.Timestamp))
                    .TransitionTo(Received),
                When(OrderAccepted)
                    .Then(context => Touch(context.Instance, context.Data.Timestamp))
                    .TransitionTo(Accepted));

            During(Received,
                When(OrderAccepted)
                    .Then(context => Touch(context.Instance, context.Data.Timestamp))
                    .TransitionTo(Accepted));

            DuringAny(
                When(OrderReceived)
                    .Then(context => Touch(context.Instance, context.Data.Timestamp))
                    .Then(context => SetReceiveTimestamp(context.Instance, context.Data.Timestamp)));
        }

        public State Received { get; private set; }
        public State Accepted { get; private set; }

        public Event<OrderReceived> OrderReceived { get; private set; }
        public Event<OrderAccepted> OrderAccepted { get; private set; }

        static void Touch(OrderState state, DateTime timestamp)
        {
            if (!state.CreateTimestamp.HasValue)
                state.CreateTimestamp = timestamp;

            if (!state.UpdateTimestamp.HasValue || state.UpdateTimestamp.Value < timestamp)
                state.UpdateTimestamp = timestamp;
        }

        static void SetReceiveTimestamp(OrderState state, DateTime timestamp)
        {
            if (!state.ReceiveTimestamp.HasValue || state.ReceiveTimestamp.Value > timestamp)
                state.ReceiveTimestamp = timestamp;
        }
    }
}