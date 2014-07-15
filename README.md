Rabbit
======

An EE framework that helps EE libraries.

This framework is meant to be integrated into another library (so that features such as login don't have to be constantly reimplemented) and is able to adjust the delay between requests to the server, depending on the location.

Furthermore, Rabbit is multithreaded so actions can be sent in the background while your app recieves events from the server in a non-blocking fashion. With GuaranteeWrite, the blocks are guaranteed to be written to your world, at least once. 
