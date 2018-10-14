## Reverse Polish Notation

Reverse Polish Notation - C# implementation.<br/>
Supported Operators: +, -, *, /, % (mod) and ^ (exponent).

### Usage

```c#
string rpn = GetRPN("5*(12+8)/2"); //output => "5 12 8 + * 2 /"
double result = CalcRPN(rpn); //output => 50
```
