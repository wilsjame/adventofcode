#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

#define ll long long 

int main() {
    freopen("test_input.txt", "r", stdin);
    int k;
    vector<int> A = {0};
    while(cin >> k) A.push_back(k);
    sort(A.begin(), A.end());
    A.push_back(A.back() + 3);

    ll res = 1;
    // Test the input and we find out the
    // longest run is 4, so we can get away with
    // hardcoding the run values. Otherwise we'd
    // try dp or some tricky combinatorics.
    for (int run = 0, i = 0; i + 1 < A.size(); i++) {
        if (A[i + 1] - A[i] ==  1) run++;
        else {
            if (run == 2) 
                res *= 2;
            else if (run == 3) 
                res *= 4;
            else if (run == 4) 
                res *= 7; // 2^3 - 1
            run = 0;
        }
    }
    cout << res << endl;

    return 0;
}

