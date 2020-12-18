#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <algorithm>
using namespace std;

#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    int k;
    vector<int> A;
    cin >> S;
    for (int i = 0; i < S.size(); i++) 
        if (S[i] == ',') S[i] = ' ';
    stringstream ss(S);
    while (ss >> k) A.push_back(k);

    for (int turn = A.size() + 1; turn <= 2020; turn++) {
        int cnt = count(A.begin(), A.end(), A.back());
        if (cnt == 1) A.push_back(0);
        else {
            // find last occurence
            for (int i = A.size() - 2; i >= 0; --i) {
                if (A[i] == A.back()) {
                    A.push_back((turn - 1) - (i + 1));
                    break;
                }
            }
        }
    }
    print(A.back());

    return 0;
}
