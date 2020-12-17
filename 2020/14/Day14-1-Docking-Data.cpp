#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <unordered_map>
#include <cstdint>
using namespace std;

#define ll long long int
#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    string s, a, b, m;
    unordered_map<ll, ll> mem;
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
            for (int i = 0; i < 36; i++) {
                if (m[i] == '1') 
                    v |= (1LL<<(35 - i));
                else if (m[i] == '0') 
                    v &= ~(1LL<<(35 - i));
            }
            mem[n] = v;
        }
    }

    ll ans = 0;
    for (auto pr : mem) 
        ans += pr.second;
    print(ans);

    return 0;
}
