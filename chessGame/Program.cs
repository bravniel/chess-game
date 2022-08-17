using System;

namespace chessGame
{
    internal class Chess
    {
        static void Main(string[] args)
        {
            new ChessGameManager().startNewChessGame();
        }
    }
    class ChessGameManager
    {
        Piece[,] board;
        string inputMove;
        bool isWhiteTurn;
        int movesWithoutCapture;
        int currentBoardPieces;
        string[] boardRecord = new string[100]; // for check if was 3 same boards one after the other of same color
        public Piece[,] getBoard() { return this.board; }
        public ChessGameManager()
        {
            this.inputMove = "";
            this.isWhiteTurn = true;
            this.movesWithoutCapture = 0;
            this.currentBoardPieces = 32;
            this.boardRecord = new string[100];
            this.board = new Piece[8, 8]; // initialize the location of the tools on the board
            this.board[0, 0] = new Rook(0, 0, false, false); this.board[0, 1] = new Knight(0, 1, false); this.board[0, 2] = new Bishop(0, 2, false); this.board[0, 3] = new Queen(0, 3, false); this.board[0, 4] = new King(0, 4, false, false); this.board[0, 5] = new Bishop(0, 5, false); this.board[0, 6] = new Knight(0, 6, false); this.board[0, 7] = new Rook(0, 7, false, false);
            this.board[1, 0] = new Pawn(1, 0, false, false); this.board[1, 1] = new Pawn(1, 1, false, false); this.board[1, 2] = new Pawn(1, 2, false, false); this.board[1, 3] = new Pawn(1, 3, false, false); this.board[1, 4] = new Pawn(1, 4, false, false); this.board[1, 5] = new Pawn(1, 5, false, false); this.board[1, 6] = new Pawn(1, 6, false, false); this.board[1, 7] = new Pawn(1, 7, false, false);
            this.board[2, 0] = new EmptyPiece(2, 0, false); this.board[2, 1] = new EmptyPiece(2, 1, false); this.board[2, 2] = new EmptyPiece(2, 2, false); this.board[2, 3] = new EmptyPiece(2, 3, false); this.board[2, 4] = new EmptyPiece(2, 4, false); this.board[2, 5] = new EmptyPiece(2, 5, false); this.board[2, 6] = new EmptyPiece(2, 6, false); this.board[2, 7] = new EmptyPiece(2, 7, false);
            this.board[3, 0] = new EmptyPiece(3, 0, false); this.board[3, 1] = new EmptyPiece(3, 1, false); this.board[3, 2] = new EmptyPiece(3, 2, false); this.board[3, 3] = new EmptyPiece(3, 3, false); this.board[3, 4] = new EmptyPiece(3, 4, false); this.board[3, 5] = new EmptyPiece(3, 5, false); this.board[3, 6] = new EmptyPiece(3, 6, false); this.board[3, 7] = new EmptyPiece(3, 7, false);
            this.board[4, 0] = new EmptyPiece(4, 0, false); this.board[4, 1] = new EmptyPiece(4, 1, false); this.board[4, 2] = new EmptyPiece(4, 2, false); this.board[4, 3] = new EmptyPiece(4, 3, false); this.board[4, 4] = new EmptyPiece(4, 4, false); this.board[4, 5] = new EmptyPiece(4, 5, false); this.board[4, 6] = new EmptyPiece(4, 6, false); this.board[4, 7] = new EmptyPiece(4, 7, false);
            this.board[5, 0] = new EmptyPiece(5, 0, false); this.board[5, 1] = new EmptyPiece(5, 1, false); this.board[5, 2] = new EmptyPiece(5, 2, false); this.board[5, 3] = new EmptyPiece(5, 3, false); this.board[5, 4] = new EmptyPiece(5, 4, false); this.board[5, 5] = new EmptyPiece(5, 5, false); this.board[5, 6] = new EmptyPiece(5, 6, false); this.board[5, 7] = new EmptyPiece(5, 7, false);
            this.board[6, 0] = new Pawn(6, 0, true, false); this.board[6, 1] = new Pawn(6, 1, true, false); this.board[6, 2] = new Pawn(6, 2, true, false); this.board[6, 3] = new Pawn(6, 3, true, false); this.board[6, 4] = new Pawn(6, 4, true, false); this.board[6, 5] = new Pawn(6, 5, true, false); this.board[6, 6] = new Pawn(6, 6, true, false); this.board[6, 7] = new Pawn(6, 7, true, false);
            this.board[7, 0] = new Rook(7, 0, true, false); this.board[7, 1] = new Knight(7, 1, true); this.board[7, 2] = new Bishop(7, 2, true); this.board[7, 3] = new Queen(7, 3, true); this.board[7, 4] = new King(7, 4, true, false); this.board[7, 5] = new Bishop(7, 5, true); this.board[7, 6] = new Knight(7, 6, true); this.board[7, 7] = new Rook(7, 7, true, false);
        }
        public void gameOverMessage(bool isWhite, int finishType)
        {
            switch (finishType)
            {
                case 1:
                    Console.WriteLine("It's checkmate! Congratulations, {0} win.\n", isWhite ? "black" : "white");
                    break;
                case 2:
                    Console.WriteLine("It's a draw! -> draw by dead position.\n");
                    break;
                case 3:
                    Console.WriteLine("It's draw! -> draw by stalemate.\n");
                    break;
                case 4:
                    Console.WriteLine("It's a draw! -> draw by threefold repetition.\n");
                    break;
                case 5:
                    Console.WriteLine("It's a draw! -> draw by fifty moves.\n");
                    break;
                default:
                    break;
            }
        }
        public void startNewChessGame() // starts the game
        {
            int finishType = 0;
            bool gameOver = false;
            Console.WriteLine("   Welcome to Daniel's\n");
            Console.WriteLine("       chess game\n");
            Console.WriteLine(ToString());
            while (!gameOver)
            {
                if (isCheckmate(isWhiteTurn)) { gameOver = true; finishType = 1; } // checkmate
                if (isDrawByDeadPosition()) { gameOver = true; finishType = 2; }  // draw by dead position - חוסר יכולת לבצע מט כשאין מספיק כלים
                if (isDrawByStalemate(isWhiteTurn)) { gameOver = true; finishType = 3; } // draw by stalemate (פט)
                if (isCheck(isWhiteTurn)) { Console.WriteLine("Check on {0}!\n", isWhiteTurn ? "white" : "black"); } // check if there is check
                inputMove = nextMoveMessage(isWhiteTurn);// player input -> next move
                if (!isLegalMove(inputMove)) { continue; } // check if the input is a legal move -> if so, move the piece
                movePiece(inputMove);
                if (isDrawByThreefoldRepetition()) { gameOver = true; finishType = 4; } // draw by threefold repetition
                if (isDrawByFiftyMove()) { gameOver = true; finishType = 5; } // draw by 50 moves without any capture
                passPlayer(); // changes turn (color)
                Console.WriteLine(ToString()); // prints updated board
            }
            gameOverMessage(isWhiteTurn, finishType);
        }
        public void passPlayer() { this.isWhiteTurn = !this.isWhiteTurn; } // change to next player color
        public bool isDrawByStalemate(bool isWhiteTurn)
        {
            if (!(isCheck(isWhiteTurn)) && !(isCanDoLegalMove(isWhiteTurn))) // draw by stalemate (פט)
                return true;
            return false;
        }
        public bool isCheckmate(bool isWhiteTurn)
        {
            if (isCheck(isWhiteTurn) && !(isCanDoLegalMove(isWhiteTurn))) // checkmate
                return true;
            return false;
        }
        public void movePiece(string inputMove)
        {
            int startRow = 0, startColumn = 0, endRow = 0, endColumn = 0; // starting (x,y) position and end (x,y) position
            for (int i = 0; i < 8; i++)
            {
                if (inputMove[0] == "abcdefgh"[i])
                    startColumn = i;
                if (inputMove[1] == "12345678"[i])
                    startRow = i;
                if (inputMove[2] == "abcdefgh"[i])
                    endColumn = i;
                if (inputMove[3] == "12345678"[i])
                    endRow = i;
            }
            if (this.board[startRow, startColumn] is Rook)
                (this.board[startRow, startColumn] as Rook).setIsMoveFromStartPosition(true);
            if (this.board[startRow, startColumn] is King)
                (this.board[startRow, startColumn] as King).setIsMoveFromStartPosition(true);
            if (this.board[startRow, startColumn] is Pawn)
                if ((Math.Abs(endRow - startRow) == 2))
                    (this.board[startRow, startColumn] as Pawn).setIsFirstMoveTwoStepForward(true);
            this.board[startRow, startColumn].move(endRow, endColumn, isWhiteTurn, this.board);
            if ((this.board[endRow, endColumn] is Pawn) && (endRow == 0 || endRow == 7))
                (this.board[endRow, endColumn] as Pawn).pawnLastRow(endRow, endColumn, isWhiteTurn, this.board);
        }
        private string nextMoveMessage(bool isWhiteTurn) // next move messege which repeats after every turn. Calculates valid move input within boundaries
        {
            string input = "";
            string move = "insert your next move (doesn't matter in upper or lower case) -> column,row:";
            string rowChar = "12345678";
            string columnChar = "abcdefgh";
            while (true) // repeat until a valid move acceppted
            {
                if (isWhiteTurn)
                    Console.WriteLine("It's WHITE turn, " + move);
                else
                    Console.WriteLine("It's BLACK turn, " + move);
                input = Console.ReadLine().ToLower();
                if (input.Length != 4) // short or long input
                {
                    Console.WriteLine("Invalid input. Try again, example for a valid move: d2d3\n");
                    continue;
                }
                if (!columnChar.Contains(input[0]) || !rowChar.Contains(input[1]) || !columnChar.Contains(input[2]) || !rowChar.Contains(input[3])) // invalid input, out of board boundary
                {
                    if (columnChar.Contains(input[1]) || rowChar.Contains(input[0]) || columnChar.Contains(input[3]) || rowChar.Contains(input[2])) // invalid input -> row,column
                    {
                        Console.WriteLine("Invalid input, input reversed -> need to specify the column first and then the row. Try again, example for a valid move: d2d3\n");
                        continue;
                    }
                    Console.WriteLine("Invalid input, out of board boundary. Try again, example for a valid move: d2d3\n");
                    continue;
                }
                if (input[0] == input[2] && input[1] == input[3]) // move the piece to the same position
                {
                    Console.WriteLine("Invalid input, you trying move the piece to the same position. Try again.\n");
                    continue;
                }
                break;
            }
            return input;
        }
        public bool isLegalMove(string inputMove) // break string input to (x,y) coordinates and checks if the move is legal
        {
            int startRow = 0, startColumn = 0, endRow = 0, endColumn = 0; // starting (x,y) position and end (x,y) position
            for (int i = 0; i < 8; i++)
            {
                if (inputMove[0] == "abcdefgh"[i])
                    startColumn = i;
                if (inputMove[1] == "12345678"[i])
                    startRow = i;
                if (inputMove[2] == "abcdefgh"[i])
                    endColumn = i;
                if (inputMove[3] == "12345678"[i])
                    endRow = i;
            }
            if (this.board[startRow, startColumn] is EmptyPiece) { Console.WriteLine("Invalid move, try again.\n"); return false; }// try move empty place
            if ((startRow == endRow) && (this.board[startRow, startColumn] is King) && (Math.Abs(startColumn - endColumn) == 2)) // check if player tries to castle
            {
                bool possible = isCastlingPossible(startRow, startColumn, endRow, endColumn); // check if castle is possible
                if (!possible) { Console.WriteLine("Invalid move, try again.\n"); }
                return possible;
            }
            if (this.board[startRow, startColumn].legalMove(endRow, endColumn, this.isWhiteTurn, this.board)) // check if the move is legal
            {
                bool checkAfterMove = isCheckAfterMove(startRow, startColumn, endRow, endColumn, isWhiteTurn);
                if (checkAfterMove) { Console.WriteLine("Invalid move, try again.\n"); }
                return !checkAfterMove;
            }
            Console.WriteLine("Invalid move, try again.\n");
            return false;
        }
        public override string ToString() // print the board as string
        {
            string[,] outputBoard = new string[9, 9]; // string output of the current board
            outputBoard[0, 0] = "   ";
            outputBoard[0, 1] = " A"; outputBoard[0, 2] = " B"; outputBoard[0, 3] = " C"; outputBoard[0, 4] = " D"; outputBoard[0, 5] = " E"; outputBoard[0, 6] = " F"; outputBoard[0, 7] = " G"; outputBoard[0, 8] = " H";
            outputBoard[1, 0] = "  1"; outputBoard[2, 0] = "  2"; outputBoard[3, 0] = "  3"; outputBoard[4, 0] = "  4"; outputBoard[5, 0] = "  5"; outputBoard[6, 0] = "  6"; outputBoard[7, 0] = "  7"; outputBoard[8, 0] = "  8";
            for (int i = 1; i < 9; i++)
                for (int j = 1; j < 9; j++)
                {
                    outputBoard[i, j] += (this.board[i - 1, j - 1]).toString();
                }
            string finalBoard = "";
            // add graphical view 
            finalBoard = " ---------------------------" + "\n";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    finalBoard += outputBoard[i, j] + "|";
                finalBoard += "\n" + " ---------------------------" + "\n";
            }
            return finalBoard;
        }
        public bool isCellThreatened(int newRow, int newColumn, bool isWhite) // check if cell (x,y) is threatend by opposing player
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (this.board[row, column] is EmptyPiece) continue;
                    if ((this.board[row, column].isWhiteColor() != isWhite) && !(this.board[row, column] is EmptyPiece)) continue; // skip same piece color
                    if (newRow == row && newColumn == column) continue;
                    if (!(this.board[newRow, newColumn] is EmptyPiece)) // check if piece on (x,y) is protected (piece is threatened (x,y) cell)
                        if (this.board[row, column].legalMove(newRow, newColumn, isWhite, this.board)) { return true; }
                    if (!(this.board[row, column] is Pawn) && this.board[row, column].legalMove(newRow, newColumn, isWhite, this.board)) // check if empty cell (x,y) is threatened
                        return true;
                    if ((this.board[row, column] is Pawn) && (Math.Abs(column - newColumn)) == 1 && (Math.Abs(newRow - row) == 1)) // check if pawn positioned correctly
                    {
                        if (isWhite && row > newRow) return true;  // white pawn
                        if (!isWhite && row < newRow) return true;  // black pawn
                    }
                }
            }
            return false;
        }
        public bool isCheck(bool isWhite) // check if there is check on current player king
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (this.board[row, column] is EmptyPiece) continue;
                    if ((this.board[row, column] is King) && (this.board[row, column].isWhiteColor() == isWhite)) // check if piece is king of same color
                        if (isCellThreatened(row, column, !isWhite)) // check if king is threatened by enemy
                            return true;
                }
            }
            return false;
        }
        public Piece savePieceForCheck(int endRow, int endColumn, bool isWhite) // check if there is check after moving the piece
        {
            Piece piece = null;
            if (this.board[endRow, endColumn] is Pawn)
                piece = new Pawn(endRow, endColumn, !isWhite, (this.board[endRow, endColumn] as Pawn).getIsFirstMoveTwoStepForward());
            if (this.board[endRow, endColumn] is Knight)
                piece = new Knight(endRow, endColumn, !isWhite);
            if (this.board[endRow, endColumn] is Bishop)
                piece = new Bishop(endRow, endColumn, !isWhite);
            if (this.board[endRow, endColumn] is Rook)
                piece = new Rook(endRow, endColumn, !isWhite, (this.board[endRow, endColumn] as Rook).getIsMoveFromStartPosition());
            if (this.board[endRow, endColumn] is Queen)
                piece = new Queen(endRow, endColumn, !isWhite);
            if (this.board[endRow, endColumn] is EmptyPiece)
                piece = new EmptyPiece(endRow, endColumn, false);
            return piece;
        }
        public bool isCheckAfterMove(int startRow, int startColumn, int endRow, int endColumn, bool isWhite) // check if there is check after moving the piece
        {
            Piece piece = savePieceForCheck(endRow, endColumn, isWhite);
            if (this.board[startRow, startColumn] is King)
            {
                this.board[startRow, startColumn].move(endRow, endColumn, isWhite, this.board);
                bool isMovingToThreatenedCell = isCellThreatened(endRow, endColumn, !isWhite); // check if king moves to a threatened cell
                this.board[endRow, endColumn].move(startRow, startColumn, isWhite, this.board);
                this.board[endRow, endColumn] = piece;
                return isMovingToThreatenedCell;
            }
            if (!(this.board[endRow, endColumn] is EmptyPiece)) //check if when Piece capture enemy piece, it  moves to a threatened cell
            {
                this.board[startRow, startColumn].move(endRow, endColumn, isWhite, this.board);
                bool isCheckAfterMoving = isCheck(isWhite); // move the pieces and checks if there is check and then reverse the movement
                this.board[endRow, endColumn].move(startRow, startColumn, isWhite, this.board);
                this.board[endRow, endColumn] = piece;
                return isCheckAfterMoving;
            }
            else //check if when Piece move to empty cell, it  moves to a threatened cell
            {
                if (Math.Abs(startColumn - endColumn) == 1 && this.board[startRow, startColumn] is Pawn && (startRow == 4 || startRow == 3)) // if it is Pawn that make En Passant
                    return false;
                this.board[startRow, startColumn].move(endRow, endColumn, isWhite, this.board);
                bool isCheckAfterMoving = isCheck(isWhite);
                this.board[endRow, endColumn].move(startRow, startColumn, isWhite, this.board);
                return isCheckAfterMoving;
            }
        }
        public bool isCastlingPossible(int startRow, int startColumn, int endRow, int endColumn) // check if castling is possible
        {
            bool currentKingColor = this.board[startRow, startColumn].isWhiteColor();
            if (isCheck(currentKingColor)) return false; // if King threatened by check
            if ((this.board[startRow, startColumn] as King).getIsMoveFromStartPosition()) return false; // if king moved from his starting cell
            bool isCastlingToRight = (startColumn < endColumn) ? true : false;
            bool rookExistInStartPosition = ((isCastlingToRight == true && (this.board[startRow, endColumn + 1] is Rook) && !(this.board[startRow, endColumn + 1] as Rook).getIsMoveFromStartPosition()) // check if right Rook exist and didn't move
                                            || (isCastlingToRight == false && (this.board[startRow, endColumn - 2] is Rook) && !(this.board[startRow, endColumn - 2] as Rook).getIsMoveFromStartPosition())) // check if left Rook exist and didn't move
                                            ? true : false;
            bool pathClear = ((isCastlingToRight == true && (this.board[startRow, startColumn + 1] is EmptyPiece) && (this.board[endRow, endColumn] is EmptyPiece)) // check if right path is clear
                             || (isCastlingToRight == false && (this.board[startRow, startColumn - 1] is EmptyPiece) && (this.board[endRow, endColumn] is EmptyPiece) && (this.board[endRow, endColumn - 1] is EmptyPiece))) // check if left path is clear
                             ? true : false;
            bool cellInTheWayIsThreatened = ((isCastlingToRight == true && (isCellThreatened(startRow, startColumn + 1, !currentKingColor) || isCellThreatened(endRow, endColumn, !currentKingColor))) // check if cells in the right way is threatened
                                          || (isCastlingToRight == false && (isCellThreatened(startRow, startColumn - 1, !currentKingColor) || isCellThreatened(startRow, endColumn, !currentKingColor) || isCellThreatened(startRow, endColumn - 1, !currentKingColor)))) // check if cells in the left way is threatened
                                          ? true : false;
            if (rookExistInStartPosition && pathClear && !cellInTheWayIsThreatened)
            {
                if (isCastlingToRight)
                {
                    (this.board[startRow, endColumn + 1] as Rook).setIsMoveFromStartPosition(true);
                    this.board[startRow, endColumn + 1].move(endRow, endColumn - 1, currentKingColor, this.board); // move the Rook
                    return true;
                }
                (this.board[startRow, endColumn - 2] as Rook).setIsMoveFromStartPosition(true);
                this.board[startRow, endColumn - 2].move(endRow, endColumn + 1, currentKingColor, this.board); // move the Rook
                return true;
            }
            return false;
        }
        public bool isCanDoLegalMove(bool isWhite) // check if there is any legal move the current player can do
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (this.board[row, column] is EmptyPiece) continue;
                    if (this.board[row, column].isWhiteColor() != isWhite) continue;
                    for (int newRow = 0; newRow < 8; newRow++)
                    {
                        for (int newColumn = 0; newColumn < 8; newColumn++)
                        {
                            if ((this.board[row, column].legalMove(newRow, newColumn, isWhite, this.board)) && !(isCheckAfterMove(row, column, newRow, newColumn, isWhite)))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        public int[] countPieces(bool isWhite)
        {
            int[] whitePieces = new int[5]; int[] blackPieces = new int[5]; // array that holds the number of each color pieces
            for (int row = 0; row < 8; row++) // count pieces
            {
                for (int column = 0; column < 8; column++)
                {
                    if (this.board[row, column] is EmptyPiece)
                        continue;
                    if (this.board[row, column] is Pawn)
                    {
                        if (this.board[row, column].isWhiteColor())
                            whitePieces[0]++;
                        else
                            blackPieces[0]++;
                    }
                    if (this.board[row, column] is Rook)
                    {
                        if (this.board[row, column].isWhiteColor())
                            whitePieces[1]++;
                        else
                            blackPieces[1]++;
                    }
                    if (this.board[row, column] is Knight)
                    {
                        if (this.board[row, column].isWhiteColor())
                            whitePieces[2]++;
                        else
                            blackPieces[2]++;
                    }
                    if (this.board[row, column] is Bishop)
                    {
                        if (this.board[row, column].isWhiteColor())
                            whitePieces[3]++;
                        else
                            blackPieces[3]++;
                    }
                    if (board[row, column] is Queen)
                    {
                        if (this.board[row, column].isWhiteColor())
                            whitePieces[4]++;
                        else
                            blackPieces[4]++;
                    }
                }
            }
            return isWhite ? whitePieces : blackPieces;
        }
        public bool isDrawByDeadPosition() // check if player have enough pieces to checkmate
        {
            if (countPieces(true)[0] == 0 && countPieces(true)[1] == 0 && countPieces(true)[4] == 0 && countPieces(false)[0] == 0 && countPieces(false)[1] == 0 && countPieces(false)[4] == 0) // check if each player has zero pieces that can mate -> Pawns, Rooks, Queens 
            {
                if ((countPieces(true)[2] + countPieces(true)[3] >= 2) || (countPieces(false)[2] + countPieces(false)[3] >= 2)) // check if each player has at least 2 pieces (Knight and Bishop)
                    return false;
                else
                {
                    return true;
                }
            }
            else
                return false;
        }
        public bool isDrawByThreefoldRepetition() // check if was 3 same boards of one of the parties
        {
            int repetition;
            string currentBoard = "";
            for (int row = 0; row < 8; row++) // break board to string
            {
                for (int column = 0; column < 8; column++)
                {
                    currentBoard += board[row, column].toString();
                    if (board[row, column] is Rook)
                        currentBoard += (board[row, column] as Rook).getIsMoveFromStartPosition();
                    if (board[row, column] is King)
                        currentBoard += (board[row, column] as King).getIsMoveFromStartPosition();
                    if (board[row, column] is Pawn)
                        currentBoard += (board[row, column] as Pawn).getIsFirstMoveTwoStepForward();
                }
            }
            for (int i = 0; i < this.boardRecord.Length; i++) // add board to the history
            {
                if (boardRecord[i] == null)
                {
                    boardRecord[i] = currentBoard;
                    repetition = 0;
                    for (int j = 0; j < boardRecord.Length; j++) // count how many same boards was
                    {
                        if (boardRecord[j] == null)
                            break;
                        if (boardRecord[i] == boardRecord[j] && i != j)
                            repetition++;
                        if (repetition == 2) { return true; }
                    }
                    break;
                }
            }
            return false;
        }
        public bool isDrawByFiftyMove() // check if was 50 moves without any capture
        {
            if (this.movesWithoutCapture == 50) { return true; }
            int boardPieces = 0;
            for (int row = 0; row < 8; row++) // count pieces on the board amount
            {
                for (int column = 0; column < 8; column++)
                {
                    if (this.board[row, column] is EmptyPiece)
                        continue;
                    else
                        boardPieces++;
                }
            }
            if (this.currentBoardPieces != boardPieces) // if piece was eaten
            {
                this.movesWithoutCapture = 0;
                this.currentBoardPieces = boardPieces;
            }
            else
                this.movesWithoutCapture++;
            return false;
        }
    }
    class Piece
    {
        private bool isWhite; // the color -> True = white , False = black
        private int x; // x position on board
        private int y; // y position on board
        public Piece(int x, int y, bool color) { this.x = x; this.y = y; this.isWhite = color; }
        public int getRow() { return this.x; }
        public int getColumn() { return this.y; }
        public bool isWhiteColor() { return this.isWhite; }
        public void setColor() { this.isWhite = !isWhite; }
        public virtual string toString()
        {
            if (this.isWhite)
                return "W";
            else
                return "B";
        }
        public virtual bool legalMove(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board) { return false; } // check if move (x,y) is legal
        public virtual void move(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board) // sets the starting position to empty cell
        {
            board[this.getRow(), this.getColumn()] = new EmptyPiece(this.getRow(), this.getColumn(), false);
            return;
        }
    }
    class Queen : Piece// מלכה
    {
        public Queen(int x, int y, bool isWhite) : base(x, y, isWhite) { }
        public override string toString() { return base.toString() + "Q"; }
        public override void move(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board) // moves Queen to (x,y)
        {
            board[newRow, newColumn] = new Queen(newRow, newColumn, isWhiteTurn);
            base.move(this.getRow(), this.getColumn(), false, board);
        }

        public override bool legalMove(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            if (this.isWhiteColor() != isWhiteTurn) return false; // check if moved piece belongs to the current player turn
            if (new Rook(this.getRow(), this.getColumn(), isWhiteTurn, false).legalMove(newRow, newColumn, isWhiteTurn, board) || // Queen movement similar to Rook -> line / row
                new Bishop(this.getRow(), this.getColumn(), isWhiteTurn).legalMove(newRow, newColumn, isWhiteTurn, board)) // Queen movement similar to Bishop -> diagonal
                return true;
            else
                return false;
        }
    }
    class King : Piece// מלך
    {
        bool isMoveFromStartPosition; // if King was moved from his starting cell -> for Castling
        public King(int x, int y, bool isWhite, bool moved) : base(x, y, isWhite) { this.isMoveFromStartPosition = moved; }
        public override string toString() { return base.toString() + "K"; }
        public bool getIsMoveFromStartPosition() { return this.isMoveFromStartPosition; }
        public void setIsMoveFromStartPosition(bool moved) { this.isMoveFromStartPosition = moved; }
        public override void move(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board) // move King to (x,y)
        {
            board[newRow, newColumn] = new King(newRow, newColumn, isWhiteTurn, getIsMoveFromStartPosition()); // sets new position cell to King
            base.move(this.getRow(), this.getColumn(), false, board); // sets old position to empty cell
        }
        public override bool legalMove(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            if (this.isWhiteColor() != isWhiteTurn) // checks if moved piece belongs to the current player turn
                return false;
            if ((newRow == this.getRow() - 1 && newColumn == this.getColumn() - 1) || // if moved: up-1 and left-1 -> diagonal
                (newRow == this.getRow() - 1 && newColumn == this.getColumn()) || // if moved: up-1
                (newRow == this.getRow() - 1 && newColumn == this.getColumn() + 1) || // if moved: up-1 and right-1 -> diagonal
                (newRow == this.getRow() && newColumn == this.getColumn() - 1) || // if moved: left-1
                (newRow == this.getRow() && newColumn == this.getColumn() + 1) || // if moved: right-1
                (newRow == this.getRow() + 1 && newColumn == this.getColumn() - 1) || // if moved: down-1 and left-1 -> diagonal
                (newRow == this.getRow() + 1 && newColumn == this.getColumn()) || // if moved: down-1
                (newRow == this.getRow() + 1 && newColumn == this.getColumn() + 1)) // if moved: down-1 and right-1 -> diagonal
            {
                if ((board[newRow, newColumn] is EmptyPiece) || board[newRow, newColumn].isWhiteColor() != isWhiteTurn) // check if (x,y) is empty or has enemy piece
                    return true;
                else // if wanted place has a piece of same color
                    return false;
            }
            else
                return false;
        }
    }
    class Rook : Piece// צריח
    {
        private bool isMoveFromStartPosition; // if Rook was moved from his starting cell -> for Castling
        public Rook(int x, int y, bool isWhite, bool moved) : base(x, y, isWhite) { this.isMoveFromStartPosition = moved; }
        public override string toString() { return base.toString() + "R"; }
        public bool getIsMoveFromStartPosition() { return this.isMoveFromStartPosition; }
        public void setIsMoveFromStartPosition(bool moved) { this.isMoveFromStartPosition = moved; }
        public override void move(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board) // move Rook to (x,y) and updates the move status for Castling
        {
            board[newRow, newColumn] = new Rook(newRow, newColumn, isWhiteTurn, getIsMoveFromStartPosition()); // sets new position cell to Rook
            base.move(this.getRow(), this.getColumn(), isWhiteTurn, board); // sets old position to empty cell
        }
        public override bool legalMove(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            if (this.isWhiteColor() != isWhiteTurn) // checks if moved piece belongs to the current player turn
                return false;
            if ((this.isWhiteColor() == board[newRow, newColumn].isWhiteColor()) && !(board[newRow, newColumn] is EmptyPiece)) // if in wanted position is piece of the same color
                return false;
            if (this.getRow() != newRow && this.getColumn() != newColumn) // checks if the way is vertical or horizontical -> not horizontal
                return false;
            bool moveVertically = (newRow == this.getRow()) ? false : true;
            int nextCellDirection = ((!moveVertically && (this.getColumn() > newColumn)) || (moveVertically && (this.getRow() > newRow))) ? (-1) : 1;
            if (!moveVertically) // direction->row, left OR right in real
            {
                for (int column = this.getColumn() + nextCellDirection; column != newColumn; column += nextCellDirection)
                    if (!(board[newRow, column] is EmptyPiece)) // if there is a piece on the way
                        return false;
            }
            if (moveVertically) // direction->line, up OR down in real
            {
                for (int row = this.getRow() + nextCellDirection; row != newRow; row += nextCellDirection)
                    if (!(board[row, newColumn] is EmptyPiece)) // if there is a piece on the way
                        return false;
            }
            if ((board[newRow, newColumn] is EmptyPiece) || ((board[newRow, newColumn].isWhiteColor() != isWhiteTurn) && (!(board[newRow, newColumn] is EmptyPiece)))) // check if wanted place is empty or enemy piece
                return true;
            return false; // if wanted place has a piece of same color
        }
    }
    class Bishop : Piece// רץ
    {
        public Bishop(int x, int y, bool isWhite) : base(x, y, isWhite) { }
        public override string toString() { return base.toString() + "B"; }
        public override void move(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            board[newRow, newColumn] = new Bishop(newRow, newColumn, isWhiteTurn);
            base.move(this.getRow(), this.getColumn(), isWhiteTurn, board);
        }
        public override bool legalMove(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            if (this.isWhiteColor() != isWhiteTurn) // checks if moved piece belongs to the current player turn
                return false;
            if (newRow == this.getRow() || newColumn == this.getColumn()) // checks if (x,y) in in the same row/column -> impossible because it move only diagonally so x & y need to change
                return false;
            if ((this.isWhiteColor() == board[newRow, newColumn].isWhiteColor()) && !(board[newRow, newColumn] is EmptyPiece)) // if in wanted position is piece of the same color
                return false;

            if (Math.Abs(newRow - this.getRow()) == Math.Abs(newColumn - this.getColumn())) // if go 1 cell up/down AND 1 cell left/right
            {
                int jumpRow;
                int jumpColumn;
                if (newRow < this.getRow())
                    jumpRow = -1; // up in real
                else
                    jumpRow = 1; // down in real
                if (newColumn < this.getColumn())
                    jumpColumn = -1; // left in real
                else
                    jumpColumn = 1; // right in real
                for (int row = this.getRow() + jumpRow, column = this.getColumn() + jumpColumn; row != newRow; row += jumpRow, column += jumpColumn)
                {
                    if (!(board[row, column] is EmptyPiece)) // if there is a piece on the way
                        return false;
                }
                if ((board[newRow, newColumn] is EmptyPiece) || board[newRow, newColumn].isWhiteColor() != isWhiteTurn) // check if wanted place is empty or enemy piece
                    return true;
            }
            return false; // if wanted place has a piece of same color  
        }
    }
    class Knight : Piece// פרש
    {
        public Knight(int x, int y, bool isWhite) : base(x, y, isWhite) { }
        public override string toString() { return base.toString() + "N"; }
        public override void move(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            board[newRow, newColumn] = new Knight(newRow, newColumn, isWhiteTurn); // sets new position cell to Knight
            base.move(this.getRow(), this.getColumn(), isWhiteTurn, board); // sets old position to empty cell
        }
        public override bool legalMove(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            if (this.isWhiteColor() != isWhiteTurn) // checks if moved piece belongs to the current player turn
                return false;
            if ((newRow == this.getRow()) || (newColumn == this.getColumn())) // obvious illegal move -> legal move need to "change" x and y position
                return false;
            if ((newRow == this.getRow() - 2 && newColumn == this.getColumn() - 1) || //if moved: up-2 and left-1
                (newRow == this.getRow() - 1 && newColumn == this.getColumn() - 2) || //if moved: up-1 and left-2
                (newRow == this.getRow() + 1 && newColumn == this.getColumn() - 2) || //if moved: down-1 and left-2
                (newRow == this.getRow() + 2 && newColumn == this.getColumn() - 1) || //if moved: down-2 and left-1
                (newRow == this.getRow() + 2 && newColumn == this.getColumn() + 1) || //if moved: down-2 and right-1
                (newRow == this.getRow() + 1 && newColumn == this.getColumn() + 2) || //if moved: down-1 and right-2
                (newRow == this.getRow() - 1 && newColumn == this.getColumn() + 2) || //if moved: up-1 and right-2
                (newRow == this.getRow() - 2 && newColumn == this.getColumn() + 1)) //if moved: up-2 and right-1
            {
                if ((board[newRow, newColumn] is EmptyPiece) || board[newRow, newColumn].isWhiteColor() != isWhiteTurn) // check if wanted place is empty or enemy piece
                    return true;
            }
            return false; // if wanted place has a piece of same color
        }
    }
    class Pawn : Piece// רגלי
    {
        private bool isFirstMoveTwoStepForward; // if Rook was moved from his starting cell -> for Castling
        public Pawn(int x, int y, bool isWhite, bool firstMoveTwoStepForward) : base(x, y, isWhite) { this.isFirstMoveTwoStepForward = firstMoveTwoStepForward; }
        public override string toString() { return base.toString() + "P"; }
        public bool getIsFirstMoveTwoStepForward() { return this.isFirstMoveTwoStepForward; }
        public void setIsFirstMoveTwoStepForward(bool firstMoveTwoStepForward) { this.isFirstMoveTwoStepForward = firstMoveTwoStepForward; }
        public override bool legalMove(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board)
        {
            if (this.isWhiteColor() != isWhiteTurn) // checks if moved piece belongs to the current player turn
                return false;
            int distanceOfRows = isWhiteTurn ? (this.getRow() - newRow) : (newRow - this.getRow());
            bool isPawnInStartPosition = ((this.getRow() == 6 && isWhiteTurn) || (this.getRow() == 1 && !isWhiteTurn)) ? true : false;
            int frontCell = isWhiteTurn ? (this.getRow() - 1) : (this.getRow() + 1);
            if ((distanceOfRows == 1) && (board[newRow, newColumn] is EmptyPiece) && (Math.Abs(newColumn - this.getColumn()) == 0)) // one step forward
                return true;
            if ((distanceOfRows == 2) && isPawnInStartPosition && (board[newRow, newColumn] is EmptyPiece) && (Math.Abs(newColumn - this.getColumn()) == 0) && (board[frontCell, newColumn] is EmptyPiece)) // only at first move, two steps forward
                return true;
            if ((distanceOfRows == 1) && (Math.Abs(newColumn - this.getColumn()) == 1) && (!(board[newRow, newColumn] is EmptyPiece)) && (isWhiteTurn != board[newRow, newColumn].isWhiteColor())) // one step forward and one step to the side -> to eat    
                return true;
            if (newRow == frontCell && (Math.Abs(newColumn - this.getColumn()) == 1) && board[this.getRow(), newColumn] is Pawn
            && board[this.getRow(), newColumn].isWhiteColor() != isWhiteTurn && board[newRow, newColumn] is EmptyPiece && canPawnEnPasan(this.getRow(), this.getColumn(), newRow, newColumn, board)) // checks if Pawn can En Pasan
                return true;
            return false;
        }
        public bool canPawnEnPasan(int rowStart, int columnStart, int rowEnd, int columnEnd, Piece[,] board) // checks if pawn can En Pasan
        {
            bool isPawnInProperPositionToMove = ((board[rowStart, columnStart].isWhiteColor() == true && rowStart == 3 && rowEnd == 2) || (board[rowStart, columnStart].isWhiteColor() == false && rowStart == 4 && rowEnd == 5)) ? true : false;
            if (board[rowStart, columnStart] is Pawn && isPawnInProperPositionToMove && Math.Abs(columnStart - columnEnd) == 1) // checks if Pawn at (x,y) can En Pasan
                if ((board[rowStart, columnEnd] is Pawn) && board[rowStart, columnEnd].isWhiteColor() != board[rowStart, columnStart].isWhiteColor())
                    if ((board[rowStart, columnEnd] as Pawn).getIsFirstMoveTwoStepForward())
                        return true;
            return false;
        }
        public override void move(int newRow, int newColumn, bool isWhiteTurn, Piece[,] board) // moves Pawn to (x,y) and updates En Passant to false
        {

            base.move(this.getRow(), this.getColumn(), isWhiteTurn, board); // sets old position to empty cell
            if (this.getRow() == 3 && newRow == 2 && Math.Abs(newColumn - this.getColumn()) == 1 && board[newRow, newColumn] is EmptyPiece) // white Pawn eat black Pawn
                board[3, newColumn] = new EmptyPiece(3, newColumn, false); // sets eaten piece position to empty cell
            if (this.getRow() == 4 && newRow == 5 && Math.Abs(newColumn - this.getColumn()) == 1 && board[newRow, newColumn] is EmptyPiece) // black Pawn eat white Pawn
                board[4, newColumn] = new EmptyPiece(4, newColumn, false); // sets eaten piece position to empty cell
            board[newRow, newColumn] = new Pawn(newRow, newColumn, isWhiteTurn, getIsFirstMoveTwoStepForward());  // sets new position cell to Pawn
        }
        public void pawnLastRow(int row, int column, bool isWhiteTurn, Piece[,] board) // if pawn at last row -> transform piece to user select type
        {
            string playerChoice = "";
            int promotionPawn = 0;
            Console.WriteLine("Please enter your wanted pawn promotion type:\n 1 - Rook, 2 - Knight, 3 - Bishop, 4 - Queen");
            while (true)
            {
                playerChoice = Console.ReadLine();
                if (playerChoice.Length == 1 && (playerChoice[0].ToString() == "1" || playerChoice[0].ToString() == "2" || playerChoice[0].ToString() == "3" || playerChoice[0].ToString() == "4"))
                    promotionPawn = int.Parse(playerChoice);
                switch (promotionPawn)
                {
                    case 1:
                        board[row, column] = new Rook(row, column, isWhiteTurn, true);
                        return;
                    case 2:
                        board[row, column] = new Knight(row, column, isWhiteTurn);
                        return;
                    case 3:
                        board[row, column] = new Bishop(row, column, isWhiteTurn);
                        return;
                    case 4:
                        board[row, column] = new Queen(row, column, isWhiteTurn);
                        return;
                    default:
                        break;
                }
                Console.WriteLine("Invalid choice, try again!");
            }
        }
    }
    class EmptyPiece : Piece // defines the empty cells
    {
        public EmptyPiece(int x, int y, bool color) : base(x, y, color) { }
        public override string toString() { return "  "; }
    }
}