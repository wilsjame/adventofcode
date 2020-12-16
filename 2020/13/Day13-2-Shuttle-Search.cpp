#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <utility>
#include <algorithm>
using namespace std;

#define ll long long
#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    vector<int> A;
    vector<pair<ll,ll>> B;
    ll id, re, step;
    cin >> id; // ignore
    cin >> S;
    for (int i = 0; i < S.length(); i++) {
        if (!isdigit(S[i])) { 
            if (S[i] == ',')
                S[i] = ' ';
            else 
                S[i] = '0';
        }
    }
    stringstream ss(S);
    while (ss >> id) {
        A.push_back(id);
    }

    // t congruent to a (mod n)
    // i.e. t has a remainder of a when divided by n
    for (int i = 0; i < A.size(); i++) {
        int n = A[i];
        if (i == 0) {
            B.push_back({n, 0});
            continue;
        }
        if (n != 0) {
            int r = n - i;
            while (r < 0) r += n;
            B.push_back({n, r});
        }
    }
    // chinese remainder theorom sieve method
    sort(B.rbegin(), B.rend());
    step = B[0].first, re = B[0].second;
    for (int i = 0; i + 1 < B.size(); i++) {
        ll target = B[i + 1].second;
        for (;; re += step) {
            if (re % B[i + 1].first == target) { 
                step *= B[i + 1].first;
                break;
            }
        }

    }
    print(re);

    return 0;
}
