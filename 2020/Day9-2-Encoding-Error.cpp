#include <iostream>
#include <vector>
#include <unordered_map>
#include <cmath>
#include <algorithm>
#include <climits>
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

    ll mn, mx, sum = 0;
    for (int i = 0; i < A.size() && sum != target; i++) {
        sum = 0, mn = LLONG_MAX, mx = -1;
        for (int j = i; sum < target; j++) {
            mn = min(mn, A[j]);
            mx = max(mx, A[j]);
            sum += A[j];
        }
    }
    cout << mn + mx << endl;

    return 0;
}
