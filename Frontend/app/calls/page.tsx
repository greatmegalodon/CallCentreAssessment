import { DefaultPagination, prefetchPaginatedQuery } from "@/hooks/query";
import { HydrationBoundary, dehydrate } from "@tanstack/react-query";
import CallsTable from "./table";

const CallsPage = async () => {
  const queryClient = await prefetchPaginatedQuery("/Call", DefaultPagination);

  return (
    <main>
      <HydrationBoundary state={dehydrate(queryClient)}>
        <CallsTable />
      </HydrationBoundary>
    </main>
  );
};

export default CallsPage;
