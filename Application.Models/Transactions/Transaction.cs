namespace Models.Transactions;

public record Transaction(long AccountId, TransactionType Type, TransactionState State);
