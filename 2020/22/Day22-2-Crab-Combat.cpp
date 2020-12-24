#include <iostream>
#include <string>
#include <deque>
#include <algorithm>
#include <vector>
#include <utility>
using namespace std;

#define print(x) cerr << #x << " is " << x << endl;

pair<int, deque<int>> solve(deque<int> P1, deque<int> P2) {
    vector<deque<int>> p1_history;
    vector<deque<int>> p2_history;

    while (!P1.empty() && !P2.empty()) {
        for (auto d : p1_history) 
            if (d == P1)
                return {1, P1};
        for (auto d : p2_history) 
            if (d == P2) 
                return {1, P1};

        p1_history.push_back(P1);
        p2_history.push_back(P2);
        int p1 = P1.front(); P1.pop_front();
        int p2 = P2.front(); P2.pop_front();

        if (p1 <= P1.size() && p2 <= P2.size()) {
            deque<int> P1_copy, P2_copy;
            for (int i = 0; i < p1; i++) P1_copy.push_back(P1[i]);
            for (int i = 0; i < p2; i++) P2_copy.push_back(P2[i]);

            pair<int, deque<int>> w = solve(P1_copy, P2_copy);;

            if (w.first == 1) {
                P1.push_back(p1); 
                P1.push_back(p2);
            }
            else {
                P2.push_back(p2);
                P2.push_back(p1);
            }
        }
        else {
            if (p1 > p2) {
                P1.push_back(p1); 
                P1.push_back(p2);
            }
            else if (p2 > p1) {
                P2.push_back(p2);
                P2.push_back(p1);
            }
        }
    }
    if (P1.empty())
        return {2, P2};
    else 
        return {1, P1};
}

int main() {
    freopen("input.txt", "r", stdin);
    deque<int> P1, P2;
    string S;
    while (getline(cin, S)) {
        if (S == "Player 1:") continue;
        else if (S == "") break;
        P1.push_back(stoi(S));
    }
    while (getline(cin, S)) {
        if (S == "Player 2:") continue;
        P2.push_back(stoi(S));
    }

    pair<int, deque<int>> winner = solve(P1, P2);
    int ans = 0, t = winner.second.size();
    for (auto k : winner.second)
        ans += k * t--;
    print(ans);

    return 0;
}
