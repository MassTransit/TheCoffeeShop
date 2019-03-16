# The Coffee Shop

A sample that covers the major operations of a typical coffee shop, using various MassTransit capabilities.

> The original MassTransit sample of a coffee shop was actually written in a coffee shop back in 2008, and was based upon the article "Starbucks doesn't use 2-phase commit" by Gregor Hohpe.

## Work in Progress

*This isn't finished yet, and it still being fleshed out.*

## Contents

The sample uses the following features:

- Container using Microsoft Dependency Injection, including:
    + Convention-based registration
    + Automatic endpoint naming
- Integration with ASP.NET Core
- Logging via Microsoft Extensions Logging

The capabilities used include:

- Consumers
- State Machine Sagas (using Automatonymous)
- Courier (routing slips)
- Async Request Client
- Quartz.NET Scheduling

