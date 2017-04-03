#!/bin/bash

protoc client_to_server_messages.proto --csharp_out=ChatClient/ChatClient/ProtoBuf
protoc server_to_client_messages.proto --csharp_out=ChatClient/ChatClient/ProtoBuf

protoc client_to_server_messages.proto --csharp_out=ChatServer/ChatServer/ProtoBuf
protoc server_to_client_messages.proto --csharp_out=ChatServer/ChatServer/ProtoBuf
