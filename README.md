## Protocol Buffers
In order to use Protocol buffers, we need to first define a schema in a .proto file.
Once we have the schema ready, we need to compile the .proto file using a protoc compiler to generate a code based on the language of choice. In our case, it's CSharp.

##Compiler command
protoc generates a .cs file which contains classes that can be used with our program.

>.\protoc.exe --csharp_out=\<output directory> \<filename>.proto
>>.\protoc.exe --csharp_out=.\ \<filename>.proto
#
We also need to install few protobuff packages.
>Install-Package Google.Protobuf 
>Install-Package Google.Protobuf.Tools
