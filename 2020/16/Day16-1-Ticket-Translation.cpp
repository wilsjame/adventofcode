#include <iostream>
#include <string>
#include <unordered_map>
#include <vector>
#include <sstream>
using namespace std;

#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    int k, ans = 0;
    string S;
    unordered_map<string, int[1000 + 5]> M;
    vector<int> T;
    vector<vector<int>> NT;

    while (getline(cin, S) && S != "") { // fields
        string f = S.substr(0, S.find(':'));
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
        for (auto a : row) {
            bool valid = false;
            for (auto pr : M) {
                if (pr.second[a] == 1) {
                    valid = true;
                    break;
                }
            }
            if (!valid) ans += a;
        }
    }
    print(ans);

    return 0;
}
