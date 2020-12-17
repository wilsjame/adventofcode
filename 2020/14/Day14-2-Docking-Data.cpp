#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <unordered_map>
#include <cstdint>
#include <cmath>
#include <algorithm>
using namespace std;

#define ll long long int
#define print(x) cerr << #x << " is " << x << endl;

unordered_map<ll, ll> mem;

// generate mask combinations and mask mem address n
void solve(string m, ll n, ll v) {
    for (int i = 0; i < 36; i++) {
        if (m[i] == 'X') {
            m[i] = '0';
            ll n0 = n, n1 = n;
            n0 &= ~(1LL<<(35 - i));
            n1 |= (1LL<<(35 - i));
            solve(m, n0, v);
            solve(m, n1, v);
            return;
        }
        else if (m[i] == '1') {
            n |= (1LL<<(35 - i));
        }
    }
    // mask n;
    for (int i = 0; i < 36; i++) {
        if (m[i] == '1') 
            n |= (1LL<<(35 - i));
    }
    mem[n] = v;
}

int main() {
    freopen("input.txt", "r", stdin);
    string s, a, b, m;
    vector<string> S;
    while (getline(cin, s)) 
        S.push_back(s);

    for (auto line : S) {
        stringstream ss(line);
        ll n, v;
        ss >> a; ss >> b; ss >> b;
        if (a == "mask") {
            m = b;
        }
        else {
            a = a.substr(a.find('[') + 1); a.pop_back();
            n = stoll(a);
            v = stoll(b);
            solve(m, n, v);
        }
    }

    ll ans = 0;
    for (auto pr : mem) 
        ans += pr.second;
    print(ans);

    return 0;
}
