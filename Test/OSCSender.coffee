osc = require 'node-osc'

client = new osc.Client '127.0.0.1', 8000

sayHello = -> 
	console.log 'hello...'
	client.send '/array1/1', 42

setInterval sayHello, 500
