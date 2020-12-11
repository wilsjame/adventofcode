#include <iostream>
#include <vector>
#include <unordered_map>
#include <cmath>
using namespace std;

#define ll long long

int main() {
    freopen("input.txt", "r", stdin);
    int N = 25;
    ll k, target;
    unordered_map<ll, ll> m;
    vector<ll> A;
    while (cin >> k) A.push_back(k);
    
    for (int i = N; i < A.size(); i++) {
        m.clear();
        for (int j = i - N; j < i; j++) 
            m[A[j]]++;

        target = A[i];
        bool secure = false;
        for (auto pr : m) {
            ll d = abs(target - pr.first);
            if (m.count(d) && d != pr.first) 
                secure = true;
        }
        if (!secure) break; 
    }
    cout << target << endl;
        
    return 0;
}
