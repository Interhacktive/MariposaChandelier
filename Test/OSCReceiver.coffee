osc = require 'node-osc'

server = new osc.Server 9000, '127.0.0.1'
server.on 'message', (msg, rinfo) ->
	console.log "received message: #{msg}"

