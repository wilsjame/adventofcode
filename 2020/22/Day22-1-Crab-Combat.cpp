#include <iostream>
#include <string>
#include <deque>
#include <algorithm>
using namespace std;

#define print(x) cerr << #x << " is " << x << endl;

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

    while (!P1.empty() && !P2.empty()) {
        int p1 = P1.front(); P1.pop_front();
        int p2 = P2.front(); P2.pop_front();
        if (p1 > p2) {
            P1.push_back(p1); 
            P1.push_back(p2);
        }
        else if (p2 > p1) {
            P2.push_back(p2);
            P2.push_back(p1);
        }
    }

    int ans = 0, t = max(P1.size(), P2.size());
    if (P1.size() > 0) {
        for (auto k : P1) 
            ans += k * t--;
    }
    else if (P2.size() > 0) {
        for (auto k : P2) 
            ans += k * t--;
    }
    print(ans);

    return 0;
}
