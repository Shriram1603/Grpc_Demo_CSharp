# GRPC :

# Interface Definition Language (IDL)
The language in which services (proto file) are written is called IDL.

# Services
Services are a collection of remote methods exposed to a client.

# Stub

In the context of gRPC, a stub is a piece of code that allows a client application to invoke 
methods on a server application as if they were local calls. The stub acts as a proxy, handling
the communication between the client and the server during a remote procedure call (RPC).

A stub in distributed computing is responsible for converting parameters passed between
the client and server during an RPC. It essentially stands in place of the actual method 
that will be executed on the server, providing the appearance that the remote method is 
present locally. This abstraction simplifies the process of making remote calls, as the 
client can interact with the stub as if it were a local object.

#  A microservice and a consumer based on a service definition :

![image](https://github.com/user-attachments/assets/64a86873-909e-457a-bbbd-66a2b0f3b095)

We can understand how gRPC works through this diagram.

The proto file provides a stub to the client and a server skeleton to the server.

Once we build the server and client, to write the body of the remote methods we override the 
Methods mentioned in the proto file, which is the server skeleton.

On the client side, the stub acts as a proxy for the server, which enables us to use the server as an object
All the server methods are easily available to us (it feels just like calling any other local method).

