#include <iostream>
#include <string>
#include <sstream>
#include <set>
#include <vector>
#include <unordered_map>
#include <algorithm>
using namespace std;

#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    int N = 0;
    string S, x;
    vector<vector<string>> I, A;
    set<string> all_I, all_A, possible_I;
    while (getline(cin, S)) {
        vector<string> i, a;
        stringstream ss(S);
        while (ss  >> x) {
            if (x != "(contains") { 
                i.push_back(x);
                all_I.insert(x);
            }
            else break;
        }
        while (ss >> x) {
            x.pop_back();
            a.push_back(x);
            all_A.insert(x);
        }
        I.push_back(i);
        A.push_back(a);
        N++;
    }

    for (string allergen : all_A) {
        unordered_map<string, int> M; 
        int cnt = 0;
        for (int i = 0; i < N; i++) {
            if (count(A[i].begin(), A[i].end(), allergen)) {
                cnt++;
                for (auto ingredient : I[i]) 
                    M[ingredient]++;
            }
        }
        for (auto pr : M) 
            if (pr.second == cnt) 
                possible_I.insert(pr.first);
    }

    int ans = 0;
    for (int i = 0; i < N; i++) 
        for (string k : I[i]) 
            ans += possible_I.count(k) == 0;
    print(ans);

    return 0;
}
