#include <iostream>
#include <string>
#include <vector>
#include <sstream>
using namespace std;

#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    vector<int> A;
    int N, id, ans, mn = 1000000; 
    string S;
    cin >> N;
    cin >> S;
    for (int i = 0; i < S.length(); i++) {
        if (!isdigit(S[i])) 
            S[i] = ' ';
    }
    stringstream ss(S);
    while (ss >> id) {
        A.push_back(id);
    }

    for (auto id : A) {
        int up = ((N + id - 1) / id) * id;
        int wait = up - N;
        if (wait < mn) {
            mn = wait;
            ans = id * wait;
        }
    }
    print(ans);

    return 0;
}
