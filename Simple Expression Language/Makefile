simple_expression.exe : simple_expression.cs node_modules
	mcs simple_expression.cs

node_modules :
	npm install

clean :
	rm -f simple_expression.exe
	rm -rf node_modules
	rm -f package-lock.json