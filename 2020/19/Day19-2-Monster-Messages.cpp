#include <iostream>
#include <string>
#include <sstream>
#include <unordered_map>
#include <vector>
#include <set>
using namespace std;

#define ALETTER 1000
#define BLETTER 2000
#define print(x) cerr << #x << " is " << x << endl;

// CYK algorithm implementation 
// return true if w can be derived from the CNF grammar G 
bool solve(string w, unordered_map<int, vector<vector<int>>> G) {
    int T, N = w.length();
    set<int> dp[100][100] = {}; // only using dp[N+1][N+1]

    for (int i = 1; i <= N; i++) {
        w[i - 1] == 'a' ? T = ALETTER : T = BLETTER;
        // unit production R -> T
        for (auto R : G) {
            int rn = R.first;
            for (vector<int> rv : R.second) {
                if (rv[0] == T) 
                    dp[1][i].insert(rn);
            }
        }
    }
    for (int l = 2; l <= N; l++) {                  // length of span 
        for (int s = 1; s <= N - l + 1; s++) {      // start of span
            for (int p = 1; p <= l - 1; p++) {      // partition of span
                // production R -> RR, RR | RR
                for (auto R : G) {
                    int rn = R.first;
                    for (vector<int> rv : R.second) {
                        if (rv[0] == T) continue;
                        int r1 = rv[0], r2 = rv[1];
                        if (dp[p][s].count(r1) && dp[l - p][s + p].count(r2))
                            dp[l][s].insert(rn);
                    }
                }
            }
        }
    }

    // rule 0 is start
    return dp[N][1].count(0);
}

int main() {
    freopen("input.txt", "r", stdin);
    string S; 
    vector<string> A;
    unordered_map<int, vector<vector<int>>> M;

    while (getline(cin, S)) {
        // So this is how it's going to be?
        // Reading and formatting input like this!
        if (S == "") break;
        int k = stoi(S.substr(0, S.find(':')));
        S.erase(0, S.find(' ') + 1);
        if (S[0] == '"') {
            if (S[1] == 'a') M[k].push_back({ALETTER});
            if (S[1] == 'b') M[k].push_back({BLETTER});
        }
        else {
            string z;
            vector<int> v;
            stringstream ss(S);
            while (ss >> z) {
                if (z == "|") break;
                v.push_back(stoi(z));
            }
            M[k].push_back(v);
            v.clear();
            if (z == "|") {
                while (ss >> z) {
                    v.push_back(stoi(z));
                }
                M[k].push_back(v);
            }
        }
    }
    while (getline(cin, S)) A.push_back(S);

    // convert CFG input into CNF
    M[8] = M[42];
    M[96] = {{ALETTER}, {BLETTER}};
    M[8].push_back({42, 8});
    M[11].push_back({42, 1001});
    M[1001] = {{11, 31}};

    int ans = 0;
    for (string msg : A) 
        ans += solve(msg, M);
    print(ans);

    return 0;
}
