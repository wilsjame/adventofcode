#include <iostream>
#include <string>
#include <sstream>
#include <unordered_map>
#include <vector>
using namespace std;

#define ALETTER 1000
#define BLETTER 2000
#define print(x) cerr << #x << " is " << x << endl;

unordered_map <int, vector<vector<int>>> M;

// return longest valid sequence of characters in message
int solve(string msg, int rn) {
    //cerr << " " << msg << " " << rn << endl;
    vector<vector<int>> terminals = M[rn];

    // base case
    if (terminals[0][0] == ALETTER || terminals[0][0] == BLETTER) {
        char ts;
        terminals[0][0] == ALETTER ? ts = 'a' : ts = 'b';
        if (msg[0] == ts) return 1; 
        else return -1;
    }

    for (auto opt : terminals) {
        int acc = 0;
        for (auto r : opt) {
            int ret = solve(msg.substr(acc), r);
            if (ret == -1) {
                acc = -1;
                break;
            }
            acc += ret;
        }
        if (acc != -1) {
            // try second option in terminals if it's there
            return acc; 
        }
    }

    return -1;
}

int main() {
    freopen("input.txt", "r", stdin);
    string S; 
    vector<string> A;
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
        
    int ans = 0;
    for (string msg : A) 
        ans += solve(msg, 0) == msg.length();
    print(ans);

    return 0;
}
