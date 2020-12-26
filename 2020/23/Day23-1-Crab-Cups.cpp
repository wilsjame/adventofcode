#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    vector<int> A;
    char c;
    while (cin >> c) A.push_back(c - '0');

    int M = 100, curr_idx = 0;
    for (int move = 1; move <= M; move++) {
        int dest, curr_cup = A[curr_idx];
        vector<int> pickup(3);
        pickup[0] = A[(curr_idx + 1) % A.size()];
        pickup[1] = A[(curr_idx + 2) % A.size()];
        pickup[2] = A[(curr_idx + 3) % A.size()];

        curr_cup - 1 < 1 ? dest = 9 : dest = curr_cup - 1;
        while (count(pickup.begin(), pickup.end(), dest) > 0) 
            if (--dest < 1) dest = 9;

        vector<int> B; 
        for (int i = 0; i < 9; i++) {
            int card = A[i];
            if (!count(pickup.begin(), pickup.end(), card))
                B.push_back(card);
        }
        B.insert(find(B.begin(), B.end(), dest) + 1, pickup[2]);
        B.insert(find(B.begin(), B.end(), dest) + 1, pickup[1]);
        B.insert(find(B.begin(), B.end(), dest) + 1, pickup[0]);
        A = B;
        curr_idx = (find(A.begin(), A.end(), curr_cup) - A.begin() + 1) % A.size();
    }

    for (auto k : A) 
        cout << k << " ";

    return 0;
}
