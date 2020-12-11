#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    int N = 0, sum;
    string instruction;
    vector<string> A, B; 
    while (getline(cin, instruction)) {
        A.push_back(instruction);
        N++;
    }

    bool OK = false;
    for (int i = 0; i < A.size() && !OK; i++) {
        string op = A[i].substr(0, A[i].find(' '));
        int arg = stoi(A[i].substr(A[i].find(' ') + 1, A[i].size()));
        B = A, sum = 0;

        if (op == "jmp" || op == "nop") {
            op == "jmp" ? B[i] = "nop " + to_string(arg) : B[i] = "jmp " + to_string(arg); 

            vector<bool> visited(10000, false);
            for (int j = 0; !visited[j];) {
                op = B[j].substr(0, B[j].find(' '));
                arg = stoi(B[j].substr(B[j].find(' ') + 1, B[j].size()));

                visited[j] = true;
                if (op == "acc") 
                    sum += arg;
                else if (op == "jmp") 
                    j += arg - 1;
                if (++j >= N) {
                    OK = true;
                    break;
                }
            }
        }

    }
    cout << sum << endl;

    return 0;
}
