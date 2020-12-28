#include <iostream>
using namespace std;

#define MOD 20201227
#define ll long long
#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    ll A_pub, B_pub, A_ls, B_ls, ls, k; 
    cin >> A_pub >> B_pub;

    // loop sizes
    for (k = 1, ls = 0; k != A_pub; ls++) 
        k = (k * 7) % MOD;
    A_ls = ls;
    for (k = 1, ls = 0; k != B_pub; ls++) 
        k = (k * 7) % MOD;
    B_ls = ls;
    // encryption key 
    for (k = 1, ls = 0; ls < A_ls; ls++) 
        k = (k * B_pub) % MOD;
    print(k);
    for (k = 1, ls = 0; ls < B_ls; ls++) 
        k = (k * A_pub) % MOD;
    print(k);

    return 0;
}
