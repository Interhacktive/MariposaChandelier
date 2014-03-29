# A simple script for testing the UDP out for the lanterns, since
# I don't have the setup locally.
 
dgram = require 'dgram'
server = dgram.createSocket 'udp4'

server.on 'error', (err) ->
	console.log "server error: #{err.stack}"
	server.close()

server.on 'message', (msg, rinfo) ->
	console.log "#{rinfo.address}:#{rinfo.port}: #{msg.length} bytes"

server.on 'listening', ->
	add = server.address()
	console.log "listening on #{add.address}:#{add.port}"

server.bind 8000, 'localhost'
