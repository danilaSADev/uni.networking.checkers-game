# checkers-game
This projects is developed to learn about TCP sockets mechanism. 

Project is divided into three parts:
- Domain - storing useful information about game essentials and different configurations
- SharedDomain - represents logic available to both server and client sides.
- Server - checkers server implementation.
- Client - checkers client implementation.

To simplify process of client-to-server/server-to-client communication was developed a special handling mechanism, which listens for incoming changes from the internet (kind of TcpListener). 
