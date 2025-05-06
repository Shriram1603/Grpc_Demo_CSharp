# GRPC :


## Table of Contents

- [Introduction](#introduction)
- [gRPC Communication patterns](#grpc-communication-patterns)

## Introduction

#### Interface Definition Language (IDL)
The language in which services (proto file) are written is called IDL.

### Services
Services are a collection of remote methods exposed to a client.

### Stub

In the context of gRPC, a stub is a piece of code that allows a client application to invoke 
methods on a server application as if they were local calls. The stub acts as a proxy, handling
the communication between the client and the server during a remote procedure call (RPC).

A stub in distributed computing is responsible for converting parameters passed between
the client and server during an RPC. It essentially stands in place of the actual method 
that will be executed on the server, providing the appearance that the remote method is 
present locally. This abstraction simplifies the process of making remote calls, as the 
client can interact with the stub as if it were a local object.

###  A microservice and a consumer based on a service definition :

![image](https://github.com/user-attachments/assets/64a86873-909e-457a-bbbd-66a2b0f3b095)

We can understand how gRPC works through this diagram.

The proto file provides a stub to the client and a server skeleton to the server.

Once we build the server and client, to write the body of the remote methods we override the 
Methods mentioned in the proto file, which is the server skeleton.

On the client side, the stub acts as a proxy for the server, which enables us to use the server as an object
All the server methods are easily available to us (it feels just like calling any other local method).

## gRPC Communication patterns

-  Simple RPC (Unary RPC)
-  server-side streaming
-  client-side streaming
-  bidirectional streaming

## Simple RPC (Unary RPC)

In simple RPC, when a client
invokes a remote function of a server, the client sends a single request to the server
and gets a single response that is sent along with status details and trailing metadata

![image](https://github.com/user-attachments/assets/1bc9ddda-fc55-44c0-b029-ec526b8d04ee)

Suppose we need to build an OrderManagement service for an online retail application
based on gRPC. One of the methods that we have to implement as part of this service
is a getOrder method, where the client can retrieve an existing order by providing the
order ID. As shown in Figure, the client is sending a single request with the order
ID and the service responds with a single response that contains the order informa‐tion.
Hence, it follows the simple RPC pattern.

## Server-Streaming RPC

In simple RPC you always had a single request and single response in the communication
between the gRPC server and gRPC client. In server-side streaming RPC, the
server sends back a sequence of responses after getting the client’s request message. 
This sequence of multiple responses is known as a “stream.” After sending all the
server responses, the server marks the end of the stream by sending the server’s status
details as trailing metadata to the client.

![image](https://github.com/user-attachments/assets/67876d12-7eb4-4bb1-aacf-fd398f72d399)

Let’s take a real-world use case to understand server-side streaming further. In our
OrderManagement service suppose that we need to build an order search capability
where we can provide a search term and get the matching results (Figure 3-2). Rather
than sending all the matching orders at once, the OrderManagement service can send
the orders as and when they are found. This means the order service client will
receive multiple response messages for a single request that it has sent.

## Client-Streaming RPC

In client-streaming RPC, the client sends multiple messages to the server instead of a
single request. The server sends back a single response to the client. However, the
server does not necessarily have to wait until it receives all the messages from the client
side to send a response. Based on this logic you may send the response after reading
one or a few messages from the stream or after reading all the messages

![image](https://github.com/user-attachments/assets/9fdf50da-376a-40ce-a741-7e5a8025d056)

Let’s further extend our OrderManagement service to understand client-streaming
RPC. Suppose you want to include a new method, updateOrders, in the OrderManage
ment service to update a set of orders (Figure 3-3). Here we want to send the order list
as a stream of messages to the server and server will process that stream and send a
message with the status of the orders that are updated.

## Bidirectional-Streaming RPC

In bidirectional-streaming RPC, the client is sending a request to the server as a
stream of messages. The server also responds with a stream of messages. The call has
to be initiated from the client side, but after that, the communication is completely
based on the application logic of the gRPC client and the server. Let’s look at an
example to understand bidirectional-streaming RPC in detail. As illustrated in
Figure 3-4, in our OrderManagement service use case, suppose we need order processing
functionality where you can send a continuous set of orders (the stream of
orders) and process them into combined shipments based on the delivery location
(i.e., orders are organized into shipments based on the delivery destination).

![image](https://github.com/user-attachments/assets/99e816a4-418b-4f91-8392-0d3cce3659e3)

We can identify the following key steps of this business use case:
• The client application initiates the business use case by setting up the connection
with the server and sending call metadata (headers).

• Once the connection setup is completed, the client application sends a continuous set
of order IDs that need to be processed by the OrderManagement service.

• Each order ID is sent to the server as a separate gRPC message.

• The service processes each order for the specified order ID and organizes them
into combined shipments based on the delivery location of the order.

• A combined shipment may contain multiple orders that should be delivered to
the same destination.

• Orders are processed in batches. When the batch size is reached, all the currently
created combined shipments will be sent back to the client.

• For example, an ordered stream of four where two orders addressed to location X
and two to location Y can be denoted as X, Y, X, Y. And if the batch size is three,
then the created combined orders should be shipment [X, X], shipment [Y], ship‐
ment [Y]. These combined shipments are also sent as a stream back to the client.


The key idea behind this business use case is that once the RPC method is invoked
either the client or service can send messages at any arbitrary time. (This also
includes the end of stream markings from either of the parties.)

## Using gRPC for Microservices Communication

![image](https://github.com/user-attachments/assets/1ff62709-b0dc-4021-846d-44fc3ea07e00)

In most of the real-world use cases, these external-facing services are exposed
through an API gateway. That is the place where you apply various nonfunctional
capabilities such as security, throttling, versioning, and so on. Most such APIs leverage
protocols such as REST or GraphQL. Although it’s not very common, you may
also expose gRPC as an external-facing service, as long as the API gateway supports
exposing gRPC interfaces. The API gateway implements cross-cutting functionality
such as authentication, logging, versioning, throttling, and load balancing. By using
an API gateway with your gRPC APIs, you are able to deploy this functionality out‐
side of your core gRPC services. One of the other important aspects of this architecture
is that we can leverage multiple programming languages but share the same
service contract between then (i.e., code generation from the same gRPC service
definition). This allows us to pick the appropriate implementation technology based
on the business capability of the service.
