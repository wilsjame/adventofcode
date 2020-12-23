#include <iostream>
#include <string>
#include <sstream>
#include <set>
#include <vector>
#include <unordered_map>
#include <algorithm>
#include <utility>
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

    // find not possible ingredients (part 1)
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

    // remove not possible ingredients
    vector<vector<string>> I_new;
    for (int i = 0; i < N; i++) {
        vector<string> tmp;
        for (string k : I[i]) 
            if (possible_I.count(k) > 0) 
                tmp.push_back(k);
        I_new.push_back(tmp);
    }

    // 1:1 match ingredients and allergens
    unordered_map<string, string> ans;
    set<string> all_matched_I;
    while (ans.size() < all_A.size()) {
        for (string allergen : all_A) {
            unordered_map<string, int> M; 
            int cnt = 0;
            for (int i = 0; i < N; i++) {
                if (count(A[i].begin(), A[i].end(), allergen)) {
                    cnt++;
                    for (auto ingredient : I_new[i]) 
                        M[ingredient]++;
                }
            }
            int unique = 0;
            string matched_I;
            for (auto pr : M) {
                if (cnt == pr.second && all_matched_I.count(pr.first) == 0) {
                    unique++;
                    matched_I = pr.first;
                }
            }
            if (unique == 1) {
                all_matched_I.insert(matched_I);
                ans[allergen] = matched_I;
            }
        }
    }
    vector<pair<string, string>> final_ans;
    for (auto pr : ans) 
        final_ans.push_back({pr.first, pr.second});
    sort(final_ans.begin(), final_ans.end());
    for (auto pr : final_ans) 
        cerr << pr.second << ",";
        
    return 0;
}
