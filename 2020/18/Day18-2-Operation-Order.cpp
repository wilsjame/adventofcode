#include <iostream>
#include <string>
#include <vector>
#include <stack>
#include <cctype>
using namespace std;

#define ll long long
#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    ll ans = 0;

    while(getline(cin, S)) {
        vector<char> T;
        stack<char> op_stack;
        stack<ll> num_stack;
        for (char c : S) 
            if (c != ' ') 
                T.push_back(c);

        // Shunting-yard infix to postfix 
        for (auto tok : T) {
            if (isdigit(tok)) {
                num_stack.push(tok - '0');
            }
            else if (tok == '+' || tok == '*') {
                // precedence: addition before multiplication
                while (!op_stack.empty() && op_stack.top() != '(' && (tok == '*' && op_stack.top() == '+')) {
                    ll a = num_stack.top(); num_stack.pop();
                    ll b = num_stack.top(); num_stack.pop();
                    if (op_stack.top() == '+') num_stack.push(a + b);
                    else if (op_stack.top() == '*') num_stack.push(a * b);
                    op_stack.pop();
                }
                op_stack.push(tok);
            }
            else if (tok == '(') {
                op_stack.push(tok);
            }
            else if (tok == ')') { 
                while (!op_stack.empty() && op_stack.top() != '(') {
                    ll a = num_stack.top(); num_stack.pop();
                    ll b = num_stack.top(); num_stack.pop();
                    if (op_stack.top() == '+') num_stack.push(a + b);
                    else if (op_stack.top() == '*') num_stack.push(a * b);
                    op_stack.pop();
                }
                if (op_stack.top() == '(') op_stack.pop();
            }
        }

        // postfix to answer
        while (!op_stack.empty()) {
            ll a = num_stack.top(); num_stack.pop();
            ll b = num_stack.top(); num_stack.pop();
            if (op_stack.top() == '+') num_stack.push(a + b);
            else if (op_stack.top() == '*') num_stack.push(a * b);
            op_stack.pop();
        }
        ans += num_stack.top();
    }

    print(ans);

    return 0;
}
