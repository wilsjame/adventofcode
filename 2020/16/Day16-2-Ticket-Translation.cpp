#include <iostream>
#include <string>
#include <unordered_map>
#include <vector>
#include <sstream>
using namespace std;

#define ll long long
#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    ll k, ans = 1;
    string S, f;
    unordered_map<string, int[1000 + 5]> M;
    vector<int> T;
    vector<vector<int>> NT, VT;

    while (getline(cin, S) && S != "") { // fields
        f = S.substr(0, S.find(':'));
        S.erase(0, S.find(':') + 2);
        int a, b, c, d;
        a = stoi(S.substr(0, S.find('-')));
        S.erase(0, S.find('-') + 1);
        b = stoi(S.substr(0, S.find(' ')));
        S.erase(0, S.find('r') + 2);
        c = stoi(S.substr(0, S.find('-')));
        S.erase(0, S.find('-') + 1);
        d = stoi(S);
        for (int i = a; i <= b; i++) 
            M[f][i]++;
        for (int i = c; i <= d; i++) 
            M[f][i]++;
    }
    getline(cin, S); // ticket
    while (getline(cin, S) && S != "") {
        while (S.find(',') != string::npos) {
            k = stoi(S.substr(0, S.find(',')));
            S.erase(0, S.find(',') + 1);
            T.push_back(k);
        } k = stoi(S); T.push_back(k);
    }
    getline(cin, S); // nearby tickets
    while (getline(cin, S)) {
        vector<int> t;
        while (S.find(',') != string::npos) {
            k = stoi(S.substr(0, S.find(',')));
            S.erase(0, S.find(',') + 1);
            t.push_back(k);
        } k = stoi(S); t.push_back(k);
        NT.push_back(t);
    }
    for (auto row : NT) {
        int invalid_fields = row.size();
        for (auto a : row) {
            for (auto pr : M) {
                if (pr.second[a] == 1) {
                    invalid_fields--;
                    break;
                }
            }
        }
        if (invalid_fields == 0) VT.push_back(row);
    }

    int N = T.size();
    vector<string> fields(N);
    unordered_map<string, int[1000 + 5]>::iterator it = M.begin();
    while (it != M.end()) {
        int idx, matched_columns = 0;
        f = it->first;
        // find which column matches this field
        // in all the tickets exactly once 
        for (int i = 0; i < N; i++) {
            int matched_fields = 0;
            if (M[f][T[i]] == 1) 
                matched_fields++;
            for (int r = 0; r < VT.size(); r++) {
                if (M[f][VT[r][i]] == 1)
                    matched_fields++;
            }
            if (matched_fields == VT.size() + 1) {
                matched_columns++;
                idx = i; 
            }
        }
        if (matched_columns == 1) {
            fields[idx] = f; 
            M.erase(f); 
            VT[0][idx] = 1000 + 1; // hacky, flag column at idx as matched
            it = M.begin();
        }
        else it++;
    }

    for (int i = 0; i < N; i++) {
        if (fields[i].find("departure") != string::npos) {
            ans *= T[i];
        }
    }
    print(ans);

    return 0;
}
