#include <iostream>
#include <vector>
#include <algorithm>
#include <set>
using namespace std;

#define ll long long
#define print(x) cerr << #x << " is " << x << endl;

// 389125467 10 11 ... 1000000
// ex) right_of[9] = 1
// ex) right_of[1000000] = 3 
// ex) left_of[3] = 1000000
int right_of[1000000 + 5]; 
int left_of[1000000 + 5]; 

int main() {
    freopen("input.txt", "r", stdin);
    char c;
    vector<int> A;
    while (cin >> c) 
        A.push_back(c - '0');
    for (int c = 10; c <= 1000000; c++)
        A.push_back(c);

    left_of[A[0]] = 1000000; 
    for (int i = 1; i < A.size(); i++) {
        right_of[A[i - 1]] = A[i];
        left_of[A[i]] = A[i - 1];
    }
    right_of[1000000] = A[0];

    ll M = 10000000, curr = A[0];
    for (int move = 1; move <= M; move++) {
        set<int> pickup;
        int a = right_of[curr];
        int b = right_of[a];
        int c = right_of[b];
        int d = right_of[c]; 
        pickup.insert(a);
        pickup.insert(b);
        pickup.insert(c);

        int dest = curr - 1;
        if (dest < 1) dest = 1000000;
        while (pickup.count(dest) > 0)
            if (--dest < 1) dest = 1000000;
        int e = right_of[dest];

        right_of[curr] = d;
        left_of[d] = curr;

        right_of[dest] = a;
        left_of[a] = dest;

        right_of[c] = e;
        left_of[e] = c;

        curr = right_of[curr];
    }
    print((ll)right_of[1] * right_of[right_of[1]])

    return 0;
}
