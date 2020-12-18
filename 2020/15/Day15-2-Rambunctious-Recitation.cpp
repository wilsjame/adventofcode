#include <iostream>
#include <string>
#include <sstream>
#include <vector>
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
        
    vector<vector<int>> last_seen(30000000 + 5, vector<int>());
    for (int i = 0; i < A.size(); i++) 
        last_seen[A[i]] = {i + 1};

    for (int turn = A.size() + 1; turn <= 30000000; turn++) {
        int b = A.back();
        int cnt = last_seen[b].size();
        if (cnt == 1) {
            A.push_back(0);
            last_seen[0].push_back(turn);
        }
        else { // cnt > 1
            int z = last_seen[b].back() - last_seen[b][last_seen[b].size() - 2];
            A.push_back(z);
            last_seen[z].push_back(turn);
        }
    }
    print(A.back());

    return 0;
}
