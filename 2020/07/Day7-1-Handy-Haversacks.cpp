#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <unordered_map>
#include <sstream>
#include <utility>
using namespace std;

int ans = 0;
bool found;
unordered_map<string, vector<pair<string, int>>> adj;

void dfs(string s) {
    if (found) return;
    if (s == "shinygold") {
        found = true;
        ans++;
    }
    for (auto u : adj[s]) {
        dfs(u.first);
    }
}

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    while (getline(cin, S)) {
        string s, u, node = S.substr(0, S.find("bags"));
        node.erase(node.find(' '), 1);
        node.erase(node.find(' '), 1);
        S.erase(0, S.find("contain") + sizeof("contain"));

        stringstream ss(S);
        ss >> s;
        if (s == "no") continue;
        vector<pair<string, int>> v;
        for (int w;; ss >> s) {
            if (s.find("bag") != string::npos) {
                v.push_back({u, w});
                u = "";
                if (s.find('.') != string::npos) break;
            }
            else if (isdigit(s[0])) w = stoi(s);
            else u += s;
        }
        adj[node] = v;
        v.clear();
    }

    for (auto p: adj) {
        string node = p.first;
        found = false;
        if (node != "shinygold")
            dfs(node);
    }
    cout << ans << endl;

    return 0;
}
